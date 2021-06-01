using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcBuiltElement ToIFC(this Panel panel, IfcObjectDefinition host)
        {
            if (panel == null || host == null)
            {
                return null;
            }

            if(panel.PanelType == PanelType.CurtainWall)
            {
                return panel.ToIFC_IfcCurtainWall(host);
            }

            switch(panel.PanelGroup)
            {
                case PanelGroup.Wall:
                    return panel.ToIFC_IfcWall(host);

                case PanelGroup.Floor:
                    return panel.ToIFC_IfcSlab(host);

                case PanelGroup.Roof:
                    return panel.ToIFC_IfcRoof(host);
            }

            return null;
        }
    }
}
