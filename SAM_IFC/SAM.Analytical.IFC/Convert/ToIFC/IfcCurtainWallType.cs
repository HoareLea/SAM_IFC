using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcCurtainWallType ToIFC_IfcCurtainWallType(this Construction construction, Xbim.Common.IModel model)
        {
            if(construction == null || model == null)
            {
                return null;
            }

            IfcCurtainWallType result = model.Instances.New<IfcCurtainWallType>();
            result.SetIfcBuildingElementType(construction);
            Core.IFC.Modify.SetIfcPropertySets(result, construction);

            return result;
        }
    }
}
