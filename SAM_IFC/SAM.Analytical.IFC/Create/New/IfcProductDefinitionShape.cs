using SAM.Geometry.Spatial;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Analytical.IFC
{
    public static partial class Create
    {
        public static IfcProductDefinitionShape IfcProductDefinitionShape(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, IPartition partition, double tolerance = Core.Tolerance.Distance)
        {
            Face3D face3D = Geometry.IFC.Query.Simplify(partition?.Face3D);
            if(face3D == null)
            {
                return null;
            }

            Xbim.Common.IModel model = ifcGeometricRepresentationContext.Model;
            if (model == null)
            {
                return null;
            }

            Plane plane = face3D.GetPlane();
            if(plane == null)
            {
                return null;
            }

            IfcProductDefinitionShape result = model.Instances.New<IfcProductDefinitionShape>();

            if(partition is IHostPartition)
            {
                //Body
                Extrusion extrusion = Analytical.Query.Extrusion((IHostPartition)partition);
                if (extrusion != null)
                {
                    Transform3D transform3D = Transform3D.GetOriginToPlane(plane);
                    transform3D.Inverse();
                    extrusion = extrusion.Transform(transform3D);

                    IfcExtrudedAreaSolid ifcExtrudedAreaSolid = Geometry.IFC.Convert.ToIFC(extrusion, model, tolerance);
                    if (ifcExtrudedAreaSolid != null)
                    {
                        IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(ifcGeometricRepresentationContext, ifcExtrudedAreaSolid, Geometry.IFC.IfcDefaultContextIdentifier.Body, Geometry.IFC.IfcDefaultContextType.SweptSolid);
                        result.Representations.Add(ifcShapeRepresentation);
                    }
                }
            }


            Vector3D normal = plane.Normal;
            if (normal.AlmostEqual(Vector3D.WorldZ, tolerance) || normal.GetNegated().AlmostEqual(Vector3D.WorldZ, tolerance))
            {
                //FootPrint
                Geometry.Planar.Face2D face2D = plane.Convert(face3D);
                if (face2D != null)
                {
                    IfcIndexedPolyCurve ifcIndexedPolyCurve = Geometry.IFC.Convert.ToIFC_IfcIndexedPolyCurve(face2D.ExternalEdge2D as Geometry.Planar.ISegmentable2D, model);
                    if (ifcIndexedPolyCurve != null)
                    {
                        IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(ifcGeometricRepresentationContext, ifcIndexedPolyCurve, Geometry.IFC.IfcDefaultContextIdentifier.FootPrint, Geometry.IFC.IfcDefaultContextType.Curve2D);
                        result.Representations.Add(ifcShapeRepresentation);
                    }
                }
            }
            else
            {
                //Axis
                Geometry.Planar.Segment2D segment2D = Geometry.IFC.Query.Axis(face3D);
                if (segment2D != null)
                {
                    IfcPolyline ifcPolyline = Geometry.IFC.Convert.ToIFC_IfcPolyline(segment2D, model);
                    if (ifcPolyline != null)
                    {
                        IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(ifcGeometricRepresentationContext, ifcPolyline, Geometry.IFC.IfcDefaultContextIdentifier.Axis, Geometry.IFC.IfcDefaultContextType.Curve2D);
                        result.Representations.Add(ifcShapeRepresentation);
                    }
                }
            }

            return result;
        }

        public static IfcProductDefinitionShape IfcProductDefinitionShape(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, Space space, ArchitecturalModel architecturalModel)
        {
            if (ifcGeometricRepresentationContext == null || space == null || architecturalModel == null)
            {
                return null;
            }

            Shell shell = architecturalModel.GetShell(space);
            if (shell == null)
            {
                return null;
            }

            Transform3D transform3D = Transform3D.GetOriginTranslation(space.Location);
            transform3D.Inverse();
            shell = shell.Transform(transform3D);

            Xbim.Common.IModel model = ifcGeometricRepresentationContext.Model;
            if (model == null)
            {
                return null;
            }

            shell = Geometry.IFC.Query.Simplify(shell);

            IfcProductDefinitionShape result = model.Instances.New<IfcProductDefinitionShape>();

            IfcFacetedBrep ifcFacetedBrep = Geometry.IFC.Convert.ToIFC(shell, model);
            if (ifcFacetedBrep != null)
            {
                IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(ifcGeometricRepresentationContext, ifcFacetedBrep, Geometry.IFC.IfcDefaultContextIdentifier.Body, Geometry.IFC.IfcDefaultContextType.Brep);
                result.Representations.Add(ifcShapeRepresentation);
            }

            return result;
        }
    }
}
