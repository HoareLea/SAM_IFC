using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcBuildingElement ToIFC(this Panel panel, Xbim.Common.IModel model)
        {
            if (panel == null || model == null)
            {
                return null;
            }

            if(panel.PanelType == PanelType.CurtainWall)
            {

            }

            switch(panel.PanelGroup)
            {
                case PanelGroup.Wall:
                    return panel.ToIFC_IfcWall(model);


            }

            return null;
        }
    }
}
