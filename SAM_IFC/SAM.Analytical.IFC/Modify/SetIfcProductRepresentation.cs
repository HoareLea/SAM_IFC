using SAM.Geometry.Spatial;
using System.Linq;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcProductRepresentation(this IfcBuildingElement ifcBuildingElement, Panel panel)
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

            IfcProductDefinitionShape ifcProductDefinitionShape = Create.IfcProductDefinitionShape(ifcGeometricRepresentationContext, panel);
            if(ifcProductDefinitionShape == null)
            {
                return;
            }

            ifcBuildingElement.Representation = ifcProductDefinitionShape;

            IfcLocalPlacement ifcLocalPlacement = Geometry.IFC.Create.IfcLocalPlacement(model, new Point3D(0, 0, 0), Vector3D.WorldX, Vector3D.WorldZ);
            ifcBuildingElement.ObjectPlacement = ifcLocalPlacement;
        }
    }
}
