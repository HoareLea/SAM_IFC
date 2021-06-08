using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;

namespace SAM.Analytical.IFC
{
    public static partial class Create
    {
        public static IfcMaterialLayerSetUsage IfcMaterialLayerSetUsage(this IfcMaterialLayerSet ifcMaterialLayerSet, IfcLayerSetDirectionEnum ifcLayerSetDirectionEnum, IfcDirectionSenseEnum ifcDirectionSenseEnum, double offsetFromReferenceLine = 0)
        {
            if(ifcMaterialLayerSet == null)
            {
                return null;
            }

            Xbim.Common.IModel model = ifcMaterialLayerSet.Model;
            if(model == null)
            {
                return null;
            }
            
            IfcMaterialLayerSetUsage result = model.Instances.New<IfcMaterialLayerSetUsage>();

            result.ForLayerSet = ifcMaterialLayerSet;
            result.LayerSetDirection = ifcLayerSetDirectionEnum;
            result.DirectionSense = ifcDirectionSenseEnum;
            result.OffsetFromReferenceLine = offsetFromReferenceLine;

            return result;
        }

        public static IfcMaterialLayerSetUsage IfcMaterialLayerSetUsage(this IfcMaterialLayerSet ifcMaterialLayerSet, PanelGroup panelGroup)
        {
            IfcLayerSetDirectionEnum ifcLayerSetDirectionEnum = IfcLayerSetDirectionEnum.AXIS2;
            IfcDirectionSenseEnum ifcDirectionSenseEnum = IfcDirectionSenseEnum.POSITIVE;
            double offsetFromReferenceLine = 0;
            switch (panelGroup)
            {
                case PanelGroup.Roof:
                    ifcLayerSetDirectionEnum = IfcLayerSetDirectionEnum.AXIS3;
                    ifcDirectionSenseEnum = IfcDirectionSenseEnum.NEGATIVE;
                    break;
                
                case PanelGroup.Floor:
                    ifcLayerSetDirectionEnum = IfcLayerSetDirectionEnum.AXIS3;
                    break;

                case PanelGroup.Wall:
                    offsetFromReferenceLine = - ifcMaterialLayerSet.TotalThickness / 2;
                    break;

                case PanelGroup.Other:
                    offsetFromReferenceLine = - ifcMaterialLayerSet.TotalThickness / 2;
                    break;

                case PanelGroup.Undefined:
                    offsetFromReferenceLine = - ifcMaterialLayerSet.TotalThickness / 2;
                    break;
            }

            return IfcMaterialLayerSetUsage(ifcMaterialLayerSet, ifcLayerSetDirectionEnum, ifcDirectionSenseEnum, offsetFromReferenceLine);
        }

    }
}
