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
                return panel.ToIFC_IfcCurtainWall(model);
            }

            switch(panel.PanelGroup)
            {
                case PanelGroup.Wall:
                    return panel.ToIFC_IfcWall(model);

                case PanelGroup.Floor:
                    return panel.ToIFC_IfcSlab(model);

                case PanelGroup.Roof:
                    return panel.ToIFC_IfcSlab(model);
            }

            return null;
        }
    }
}
