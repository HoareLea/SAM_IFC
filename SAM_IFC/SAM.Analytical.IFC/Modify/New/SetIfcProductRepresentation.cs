using SAM.Geometry.Spatial;
using System.Linq;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcProductRepresentation(this IfcBuildingElement ifcBuildingElement, IPartition partition, double tolerance = Core.Tolerance.Distance)
        {
            if(ifcBuildingElement == null || partition == null)
            {
                return;
            }

            Xbim.Common.IModel model = ifcBuildingElement.Model;
            if(model == null)
            {
                return;
            }

            IfcGeometricRepresentationContext ifcGeometricRepresentationContext = model.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();

            IfcProductDefinitionShape ifcProductDefinitionShape = Create.IfcProductDefinitionShape(ifcGeometricRepresentationContext, partition, tolerance);
            if(ifcProductDefinitionShape == null)
            {
                return;
            }

            ifcBuildingElement.Representation = ifcProductDefinitionShape;

            Plane plane = partition.Face3D.GetPlane();

            IfcLocalPlacement ifcLocalPlacement = Geometry.IFC.Create.IfcLocalPlacement(model, plane);
            ifcBuildingElement.ObjectPlacement = ifcLocalPlacement;
        }

        public static void SetIfcProductRepresentation(this IfcSpace ifcSpace, Space space, BuildingModel buildingModel)
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

            IfcProductDefinitionShape ifcProductDefinitionShape = Create.IfcProductDefinitionShape(ifcGeometricRepresentationContext, space, buildingModel);
            if (ifcProductDefinitionShape != null)
            {
                ifcSpace.Representation = ifcProductDefinitionShape;
            }

            ifcSpace.ObjectPlacement = Geometry.IFC.Create.IfcLocalPlacement(model, space.Location);
        }
    }
}
