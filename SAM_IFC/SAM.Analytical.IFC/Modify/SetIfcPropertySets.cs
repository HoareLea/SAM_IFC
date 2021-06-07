using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcPropertySets(this IfcWall ifcWall, Panel panel)
        {
            if(ifcWall == null || panel == null)
            {
                return;
            }
            
            Core.IFC.Modify.SetIfcPropertySets(ifcWall, panel);

            ifcWall.SetPropertySingleValue("Pset_WallCommon", "IsExternal", new IfcBoolean(Analytical.Query.External(panel.PanelType)));

        }
    }
}
