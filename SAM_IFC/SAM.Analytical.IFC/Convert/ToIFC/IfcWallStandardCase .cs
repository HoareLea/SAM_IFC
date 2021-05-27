using System.Linq;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcWallStandardCase ToIFC_IfcWallStandardCase(this Panel panel, Xbim.Common.IModel model)
        {
            if(panel == null || model == null)
            {
                return null;
            }

            IfcWallStandardCase result = model.Instances.New<IfcWallStandardCase>();
            //result.GlobalId = new Xbim.Ifc4.UtilityResource.IfcGloballyUniqueId(panel.Guid.ToString());

            IfcGeometricRepresentationContext ifcGeometricRepresentationContext = model.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();

            IfcProductDefinitionShape ifcProductDefinitionShape = Create.IfcProductDefinitionShape(ifcGeometricRepresentationContext, panel);
            result.Representation = ifcProductDefinitionShape;

            IfcLocalPlacement ifcLocalPlacement = Geometry.IFC.Create.IfcLocalPlacement(model, new Geometry.Spatial.Point3D(0, 0, 0), Geometry.Spatial.Vector3D.WorldX, Geometry.Spatial.Vector3D.WorldZ);
            result.ObjectPlacement = ifcLocalPlacement;

            return result;
        }
    }
}
