using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcBuildingElementType(this IfcBuildingElementType ifcBuildingElementType, Construction construction)
        {
            if(ifcBuildingElementType == null || construction == null)
            {
                return;
            }

            ifcBuildingElementType.GlobalId = construction.Guid;
            ifcBuildingElementType.Name = construction.Name;
            ifcBuildingElementType.Description = Core.IFC.Query.Description(construction);
        }
    }
}
