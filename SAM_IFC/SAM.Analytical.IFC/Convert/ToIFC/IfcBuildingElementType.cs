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

            if(panelType_Temp == PanelType.CurtainWall)
            {
                return ToIFC_IfcCurtainWallType(construction, model);
            }
            
            switch(panelType_Temp.PanelGroup())
            {
                case PanelGroup.Wall:
                    return ToIFC_IfcWallType(construction, model);

                case PanelGroup.Roof:
                    return ToIFC_IfcRoofType(construction, model);

                case PanelGroup.Floor:
                    return ToIFC_IfcSlabType(construction, model);

            }

            return null;
        }
    }
}
