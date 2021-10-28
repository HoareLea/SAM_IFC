using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcBuildingElement(this IfcBuildingElement ifcBuildingElement, IPartition partition)
        {
            if(ifcBuildingElement == null || partition == null)
            {
                return;
            }

            ifcBuildingElement.Name = partition.Name;
            ifcBuildingElement.GlobalId = partition.Guid;
            ifcBuildingElement.Description = Core.IFC.Query.Description(partition as Core.SAMObject);
            //ifcBuildingElement.ObjectType = typeof(Panel).ToString();
        }
    }
}
