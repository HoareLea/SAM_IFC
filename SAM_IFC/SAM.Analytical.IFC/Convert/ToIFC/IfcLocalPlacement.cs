using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcLocalPlacement ToIFC_IfcLocalPlacement(this Geometry.Spatial.Point3D point3D, Xbim.Common.IModel model)
        {
            if(point3D == null || model == null)
            {
                return null;
            }

            IfcLocalPlacement result = model.Instances.New<IfcLocalPlacement>();

            IfcAxis2Placement3D ifcAxis2Placement3D = Geometry.IFC.Convert.ToIFC_IfcAxis2Placement3D(point3D, model);
            result.RelativePlacement = ifcAxis2Placement3D;

            return result;
        }
    }
}
