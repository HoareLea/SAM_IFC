using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static IIfcPropertySingleValue SetIsExternal(this IfcBuildingElement ifcBuildingElement, IPartition partition, ArchitecturalModel architecturalModel)
        {
            if (ifcBuildingElement == null || partition == null || architecturalModel == null)
            {
                return null;
            }

            return SetIsExternal(ifcBuildingElement as dynamic, architecturalModel.External(partition));
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcBuildingElementType ifcBuildingElementType, PartitionAnalyticalType partitionAnalyticalType)
        {
            return SetIsExternal(ifcBuildingElementType as dynamic, Analytical.Query.External(partitionAnalyticalType));
        }
    }
}
