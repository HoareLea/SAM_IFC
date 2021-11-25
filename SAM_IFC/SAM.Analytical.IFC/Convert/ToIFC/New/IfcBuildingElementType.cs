using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcBuildingElementType ToIFC_IfcBuildingElementType(this IHostPartition hostPartition, Xbim.Common.IModel model, BuildingModel buildingModel)
        {
            if (hostPartition == null || model == null)
            {
                return null;
            }

            return ToIFC(hostPartition.Type(), model, buildingModel);
        }

        public static IfcBuildingElementType ToIFC(this HostPartitionType hostPartitionType, Xbim.Common.IModel model, BuildingModel buildingModel)
        {
            if(hostPartitionType == null || model == null)
            {
                return null;
            }

            IfcBuildingElementType result = null;

            if (hostPartitionType is WallType)
            {
                if(buildingModel != null)
                {
                    if(buildingModel.GetMaterialType(hostPartitionType) == Core.MaterialType.Transparent)
                    {
                        return ToIFC_IfcCurtainWallType((WallType)hostPartitionType, model);
                    }
                }
                
                return ToIFC_IfcWallType((WallType)hostPartitionType, model);
            }

            if(hostPartitionType is FloorType)
            {
                return ToIFC_IfcSlabType((FloorType)hostPartitionType, model);
            }

            if(hostPartitionType is RoofType)
            {
                return ToIFC_IfcSlabType((RoofType)hostPartitionType, model);
            }

            return result;
        }
    }
}
