using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcBuildingElement(this IfcBuildingElement ifcBuildingElement, Panel panel)
        {
            if(ifcBuildingElement == null || panel == null)
            {
                return;
            }

            ifcBuildingElement.Name = panel.Name;
            ifcBuildingElement.GlobalId = panel.Guid;
            ifcBuildingElement.Description = Core.IFC.Query.Description(panel);
            //ifcBuildingElement.ObjectType = typeof(Panel).ToString();
        }
    }
}
