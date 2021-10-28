using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSlabType ToIFC_IfcSlabType(this FloorType floorType, Xbim.Common.IModel model)
        {
            if(floorType == null || model == null)
            {
                return null;
            }

            IfcSlabType result = model.Instances.New<IfcSlabType>();
            result.PredefinedType = Xbim.Ifc4.Interfaces.IfcSlabTypeEnum.FLOOR;
            result.SetIfcBuildingElementType(floorType);
            Core.IFC.Modify.SetIfcPropertySets(result, floorType);

            return result;
        }

        public static IfcSlabType ToIFC_IfcSlabType(this RoofType roofType, Xbim.Common.IModel model)
        {
            if (roofType == null || model == null)
            {
                return null;
            }

            IfcSlabType result = model.Instances.New<IfcSlabType>();
            result.PredefinedType = Xbim.Ifc4.Interfaces.IfcSlabTypeEnum.ROOF;
            result.SetIfcBuildingElementType(roofType);
            Core.IFC.Modify.SetIfcPropertySets(result, roofType);

            return result;
        }
    }
}
