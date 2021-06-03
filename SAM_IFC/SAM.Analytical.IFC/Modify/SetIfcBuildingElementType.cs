using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcBuildingElementType(this IfcBuiltElementType ifcBuiltElementType, Construction construction)
        {
            if(ifcBuiltElementType == null || construction == null)
            {
                return;
            }

            ifcBuiltElementType.Guid = construction.Guid;
            ifcBuiltElementType.Name = construction.Name;
            ifcBuiltElementType.Description = Core.IFC.Query.Description(construction);
        }
    }
}
