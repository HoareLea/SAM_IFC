using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcBuilding ToIFC_IfcBuilding(this AnalyticalModel analyticalModel, Xbim.Common.IModel model)
        {
            if(analyticalModel == null || model == null)
            {
                return null;
            }

            IfcBuilding result = model.Instances.New<IfcBuilding>();
            result.Name = analyticalModel.Name;
            result.CompositionType = Xbim.Ifc4.Interfaces.IfcElementCompositionEnum.ELEMENT;

            Geometry.Spatial.Point3D point3D = Geometry.Spatial.Create.Point3D(0, 0, 0);
            Xbim.Ifc4.GeometricConstraintResource.IfcLocalPlacement ifcLocalPlacement = Geometry.IFC.Create.IfcLocalPlacement(model, point3D);
            result.ObjectPlacement = ifcLocalPlacement;

            return result;
        }
    }
}
