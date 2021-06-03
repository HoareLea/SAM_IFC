using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcBuildingElementType ToIFC_IfcBuildingElementType(this Panel panel, Xbim.Common.IModel model)
        {
            if (panel == null || model == null)
            {
                return null;
            }

            return ToIFC(panel.Construction, model, panel.PanelType);
        }

        public static IfcBuildingElementType ToIFC(this Construction construction, Xbim.Common.IModel model, PanelType panelType = PanelType.Undefined)
        {
            if(construction == null || model == null)
            {
                return null;
            }

            PanelType panelType_Temp = panelType;

            if(panelType_Temp == PanelType.Undefined)
                panelType_Temp = construction.PanelType();

            IfcBuildingElementType result = null;

            if (panelType_Temp == PanelType.CurtainWall)
            {
                result = ToIFC_IfcCurtainWallType(construction, model);
            }
            else
            {
                switch (panelType_Temp.PanelGroup())
                {
                    case PanelGroup.Wall:
                        result = ToIFC_IfcWallType(construction, model);
                        break;

                    case PanelGroup.Roof:
                        result = ToIFC_IfcSlabType(construction, model, panelType.PanelGroup());
                        break;

                    case PanelGroup.Floor:
                        result = ToIFC_IfcSlabType(construction, model);
                        break;

                }
            }

            return result;
        }
    }
}
