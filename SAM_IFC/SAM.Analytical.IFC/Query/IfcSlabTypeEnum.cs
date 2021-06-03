using Xbim.Ifc4.Interfaces;

namespace SAM.Analytical.IFC
{
    public static partial class Query
    {
        public static IfcSlabTypeEnum IfcSlabTypeEnum(this PanelGroup panelGroup)
        {
            IfcSlabTypeEnum result = Xbim.Ifc4.Interfaces.IfcSlabTypeEnum.NOTDEFINED;
            switch (panelGroup)
            {
                case PanelGroup.Floor:
                    result = Xbim.Ifc4.Interfaces.IfcSlabTypeEnum.FLOOR;
                    break;
                case PanelGroup.Roof:
                    result = Xbim.Ifc4.Interfaces.IfcSlabTypeEnum.ROOF;
                    break;
            }

            return result;
        }
    }
}
