using Xbim.Ifc;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.ProfileResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcParameterizedProfileDef ToIFC_IfcParameterizedProfileDef(this Spatial.Rectangle3D rectangle3D, IfcStore ifcStore)
        {
            if(rectangle3D == null || ifcStore == null)
            {
                return null;
            }

            IfcParameterizedProfileDef result = ifcStore.Instances.New<IfcParameterizedProfileDef>();
            //result.Position =

            return result;
        }
    }
}
