using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProfileResource;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcArbitraryClosedProfileDef IfcArbitraryClosedProfileDef(this Xbim.Common.IModel model, Planar.Polygon2D polygon2D, IfcProfileTypeEnum ifcProfileTypeEnum)
        {
            if(polygon2D == null || model == null)
            {
                return null;
            }

            IfcArbitraryClosedProfileDef result = model.Instances.New<IfcArbitraryClosedProfileDef>();
            result.OuterCurve = polygon2D.ToIFC(model);
            result.ProfileType = ifcProfileTypeEnum;

            return result;
        }
    }
}
