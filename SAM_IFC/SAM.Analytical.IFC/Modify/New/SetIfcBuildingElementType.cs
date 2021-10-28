using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcBuildingElementType(this IfcBuildingElementType ifcBuildingElementType, HostPartitionType hostPartitionType)
        {
            if(ifcBuildingElementType == null || hostPartitionType == null)
            {
                return;
            }

            ifcBuildingElementType.GlobalId = hostPartitionType.Guid;
            ifcBuildingElementType.Name = hostPartitionType.Name;
            ifcBuildingElementType.Description = Core.IFC.Query.Description(hostPartitionType);
        }
    }
}
