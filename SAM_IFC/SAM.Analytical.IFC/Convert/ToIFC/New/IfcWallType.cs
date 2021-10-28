using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcWallType ToIFC_IfcWallType(this WallType wallType, Xbim.Common.IModel model)
        {
            if(wallType == null || model == null)
            {
                return null;
            }

            IfcWallType result = model.Instances.New<IfcWallType>();
            result.PredefinedType = Xbim.Ifc4.Interfaces.IfcWallTypeEnum.STANDARD;
            result.SetIfcBuildingElementType(wallType);
            Core.IFC.Modify.SetIfcPropertySets(result, wallType);

            return result;
        }
    }
}
