using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcCurtainWallType ToIFC_IfcCurtainWallType(this WallType wallType, Xbim.Common.IModel model)
        {
            if(wallType == null || model == null)
            {
                return null;
            }

            IfcCurtainWallType result = model.Instances.New<IfcCurtainWallType>();
            result.PredefinedType = Xbim.Ifc4.Interfaces.IfcCurtainWallTypeEnum.NOTDEFINED;
            result.SetIfcBuildingElementType(wallType);
            Core.IFC.Modify.SetIfcPropertySets(result, wallType);

            return result;
        }
    }
}
