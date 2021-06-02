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

            //ifcBuiltElementType.GlobalId = construction.Guid.ToString("N");
            ifcBuiltElementType.Name = construction.Name;
            ifcBuiltElementType.Description = Core.IFC.Query.Description(construction);
        }
    }
}
