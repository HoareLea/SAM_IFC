using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcBuildingElement(this IfcBuiltElement ifcBuiltElement, Panel panel)
        {
            if(ifcBuiltElement == null || panel == null)
            {
                return;
            }

            ifcBuiltElement.Name = panel.Name;
            ifcBuiltElement.GlobalId = panel.Guid.ToString("N");
            ifcBuiltElement.Description = Core.IFC.Query.Description(panel);
            //ifcBuildingElement.ObjectType = typeof(Panel).ToString();
        }
    }
}
