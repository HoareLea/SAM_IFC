using SAM.Geometry.Spatial;
using System.Linq;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcProductRepresentation(this IfcBuildingElement ifcBuildingElement, Panel panel, double tolerance = Core.Tolerance.Distance)
        {
            if(ifcBuildingElement == null || panel == null)
            {
                return;
            }

            Xbim.Common.IModel model = ifcBuildingElement.Model;
            if(model == null)
            {
                return;
            }

            IfcGeometricRepresentationContext ifcGeometricRepresentationContext = model.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();

            Plane plane = panel.Plane;

            IfcProductDefinitionShape ifcProductDefinitionShape = Create.IfcProductDefinitionShape(ifcGeometricRepresentationContext, panel, tolerance);
            if(ifcProductDefinitionShape == null)
            {
                return;
            }

            ifcBuildingElement.Representation = ifcProductDefinitionShape;

            IfcLocalPlacement ifcLocalPlacement = Geometry.IFC.Create.IfcLocalPlacement(model, plane);
            ifcBuildingElement.ObjectPlacement = ifcLocalPlacement;
        }

        public static void SetIfcProductRepresentation(this IfcSpace ifcSpace, Space space, AdjacencyCluster adjacencyCluster = null)
        {
            if (ifcSpace == null || space == null)
            {
                return;
            }

            Xbim.Common.IModel model = ifcSpace.Model;
            if (model == null)
            {
                return;
            }

            IfcGeometricRepresentationContext ifcGeometricRepresentationContext = model.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();

            IfcProductDefinitionShape ifcProductDefinitionShape = Create.IfcProductDefinitionShape(ifcGeometricRepresentationContext, space, adjacencyCluster);
            if (ifcProductDefinitionShape != null)
            {
                ifcSpace.Representation = ifcProductDefinitionShape;
            }

            ifcSpace.ObjectPlacement = Geometry.IFC.Create.IfcLocalPlacement(model, space.Location);
        }
    }
}
