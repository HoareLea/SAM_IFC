using SAM.Geometry.Spatial;
using GeometryGym.Ifc;
using System.Collections.Generic;

namespace SAM.Analytical.IFC
{
    public static partial class Create
    {
        public static IfcProductDefinitionShape IfcProductDefinitionShape(this DatabaseIfc databaseIfc, Panel panel, double tolerance = Core.Tolerance.Distance)
        {
            Face3D face3D = panel?.GetFace3D();
            if(face3D == null || databaseIfc == null)
            {
                return null;
            }

            Plane plane = panel.Plane;
            if(plane == null)
            {
                return null;
            }

            List<IfcShapeModel> ifcShapeModels = new List<IfcShapeModel>();

            //Body
            Extrusion extrusion = Query.Extrusion(panel);
            if(extrusion != null)
            {
                Transform3D transform3D = Transform3D.GetOriginToPlane(plane);
                transform3D.Inverse();
                extrusion = extrusion.Transform(transform3D);

                IfcExtrudedAreaSolid ifcExtrudedAreaSolid = Geometry.IFC.Convert.ToIFC(extrusion, databaseIfc, tolerance);
                if (ifcExtrudedAreaSolid != null)
                {
                    IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(databaseIfc, ifcExtrudedAreaSolid, IfcGeometricRepresentationSubContext.SubContextIdentifier.Body, ShapeRepresentationType.SweptSolid);
                    ifcShapeModels.Add(ifcShapeRepresentation);
                }
            }

            Vector3D normal = plane.Normal;
            if (normal.AlmostEqual(Vector3D.WorldZ, tolerance) || normal.GetNegated().AlmostEqual(Vector3D.WorldZ, tolerance))
            {
                //FootPrint
                Geometry.Planar.Face2D face2D = plane.Convert(face3D);
                if (face2D != null)
                {
                    IfcIndexedPolyCurve ifcIndexedPolyCurve = Geometry.IFC.Convert.ToIFC_IfcIndexedPolyCurve(face2D.ExternalEdge2D as Geometry.Planar.ISegmentable2D, databaseIfc);
                    if (ifcIndexedPolyCurve != null)
                    {
                        IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(databaseIfc, ifcIndexedPolyCurve, IfcGeometricRepresentationSubContext.SubContextIdentifier.FootPrint, ShapeRepresentationType.Curve2D);
                        ifcShapeModels.Add(ifcShapeRepresentation);
                    }
                }
            }
            else
            {
                //Axis
                Geometry.Planar.Segment2D segment2D = Geometry.IFC.Query.Axis(face3D);
                if (segment2D != null)
                {
                    IfcPolyline ifcPolyline = Geometry.IFC.Convert.ToIFC_IfcPolyline(segment2D, databaseIfc);
                    if (ifcPolyline != null)
                    {
                        IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(databaseIfc, ifcPolyline, IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis, ShapeRepresentationType.Curve2D);
                        ifcShapeModels.Add(ifcShapeRepresentation);
                    }
                }
            }

            IfcProductDefinitionShape result = new IfcProductDefinitionShape(ifcShapeModels);
            return result;
        }

        public static IfcProductDefinitionShape IfcProductDefinitionShape(this DatabaseIfc databaseIfc, Space space, AdjacencyCluster adjacencyCluster = null)
        {
            if(databaseIfc == null || space == null || adjacencyCluster == null)
            {
                return null;
            }

            Shell shell = adjacencyCluster.Shell(space);
            if(shell == null)
            {
                return null;
            }

            List<IfcShapeModel> ifcShapeModels = new List<IfcShapeModel>();

            IfcFacetedBrep ifcFacetedBrep = Geometry.IFC.Convert.ToIFC(shell, databaseIfc);
            if(ifcFacetedBrep != null)
            {
                IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(databaseIfc, ifcFacetedBrep, IfcGeometricRepresentationSubContext.SubContextIdentifier.Body, ShapeRepresentationType.Brep);
                ifcShapeModels.Add(ifcShapeRepresentation);
            }

            IfcProductDefinitionShape result = new IfcProductDefinitionShape(ifcShapeModels);
            return result;
        }
    }
}
