using SAM.Geometry.Spatial;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Analytical.IFC
{
    public static partial class Create
    {
        public static IfcProductDefinitionShape IfcProductDefinitionShape(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, Panel panel, double tolerance = Core.Tolerance.Distance)
        {
            Face3D face3D = panel?.GetFace3D();
            if(face3D == null)
            {
                return null;
            }

            Xbim.Common.IModel model = ifcGeometricRepresentationContext.Model;
            if (model == null)
            {
                return null;
            }

            IfcProductDefinitionShape result = model.Instances.New<IfcProductDefinitionShape>();

            Extrusion extrusion = Query.Extrusion(panel);
            if(extrusion != null)
            {
                IfcExtrudedAreaSolid ifcExtrudedAreaSolid = Geometry.IFC.Convert.ToIFC(extrusion, model, tolerance);
                if (ifcExtrudedAreaSolid != null)
                {
                    IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(ifcGeometricRepresentationContext, ifcExtrudedAreaSolid, Geometry.IFC.IfcDefaultContextType.SweptSolid, Geometry.IFC.IfcDefaultContextIdentifier.Body);
                    result.Representations.Add(ifcShapeRepresentation);
                }
            }

            return result;
        }

        public static IfcProductDefinitionShape IfcProductDefinitionShape(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, Space space, AdjacencyCluster adjacencyCluster = null)
        {
            if(ifcGeometricRepresentationContext == null || space == null || adjacencyCluster == null)
            {
                return null;
            }

            Shell shell = adjacencyCluster.Shell(space);
            if(shell == null)
            {
                return null;
            }

            Xbim.Common.IModel model = ifcGeometricRepresentationContext.Model;
            if (model == null)
            {
                return null;
            }

            IfcProductDefinitionShape result = model.Instances.New<IfcProductDefinitionShape>();

            IfcFacetedBrep ifcFacetedBrep = Geometry.IFC.Convert.ToIFC(shell, model);
            if(ifcFacetedBrep != null)
            {
                IfcShapeRepresentation ifcShapeRepresentation = Geometry.IFC.Create.IfcShapeRepresentation(ifcGeometricRepresentationContext, ifcFacetedBrep, Geometry.IFC.IfcDefaultContextType.Brep, Geometry.IFC.IfcDefaultContextIdentifier.Body);
                result.Representations.Add(ifcShapeRepresentation);
            }

            return result;
        }
    }
}
