using Xbim.Ifc;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.ProfileResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcParameterizedProfileDef ToIFC_IfcParameterizedProfileDef(this Spatial.Rectangle3D rectangle3D, Xbim.Common.IModel model)
        {
            if(rectangle3D == null || model == null)
            {
                return null;
            }

            IfcParameterizedProfileDef result = model.Instances.New<IfcParameterizedProfileDef>();
            //result.Position =

            return result;
        }
    }
}
