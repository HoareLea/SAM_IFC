using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcBuiltElementType ToIFC_IfcBuiltElementType(this Panel panel, DatabaseIfc databaseIfc)
        {
            if (panel == null || databaseIfc == null)
            {
                return null;
            }

            return ToIFC(panel.Construction, databaseIfc, panel.PanelType);
        }

        public static IfcBuiltElementType ToIFC(this Construction construction, DatabaseIfc databaseIfc, PanelType panelType = PanelType.Undefined)
        {
            if(construction == null || databaseIfc == null)
            {
                return null;
            }

            PanelType panelType_Temp = panelType;

            if(panelType_Temp == PanelType.Undefined)
                panelType_Temp = construction.PanelType();

            IfcBuiltElementType result = null;

            if (panelType_Temp == PanelType.CurtainWall)
            {
                result = ToIFC_IfcCurtainWallType(construction, databaseIfc);
            }
            else
            {
                switch (panelType_Temp.PanelGroup())
                {
                    case PanelGroup.Wall:
                        result = ToIFC_IfcWallType(construction, databaseIfc);
                        break;

                    case PanelGroup.Roof:
                        result = ToIFC_IfcRoofType(construction, databaseIfc);
                        break;

                    case PanelGroup.Floor:
                        result = ToIFC_IfcSlabType(construction, databaseIfc);
                        break;

                }
            }

            return result;
        }
    }
}
