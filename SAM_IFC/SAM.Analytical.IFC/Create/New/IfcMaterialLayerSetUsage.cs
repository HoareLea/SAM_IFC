using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;

namespace SAM.Analytical.IFC
{
    public static partial class Create
    {
        public static IfcMaterialLayerSetUsage IfcMaterialLayerSetUsage(this IfcMaterialLayerSet ifcMaterialLayerSet, HostPartitionCategory hostPartitionCategory)
        {
            IfcLayerSetDirectionEnum ifcLayerSetDirectionEnum = IfcLayerSetDirectionEnum.AXIS2;
            IfcDirectionSenseEnum ifcDirectionSenseEnum = IfcDirectionSenseEnum.POSITIVE;
            double offsetFromReferenceLine = 0;
            switch (hostPartitionCategory)
            {
                case HostPartitionCategory.Roof:
                    ifcLayerSetDirectionEnum = IfcLayerSetDirectionEnum.AXIS3;
                    ifcDirectionSenseEnum = IfcDirectionSenseEnum.NEGATIVE;
                    break;
                
                case HostPartitionCategory.Floor:
                    ifcLayerSetDirectionEnum = IfcLayerSetDirectionEnum.AXIS3;
                    break;

                case HostPartitionCategory.Wall:
                    offsetFromReferenceLine = - ifcMaterialLayerSet.TotalThickness / 2;
                    break;

                case HostPartitionCategory.Undefined:
                    offsetFromReferenceLine = - ifcMaterialLayerSet.TotalThickness / 2;
                    break;
            }

            return IfcMaterialLayerSetUsage(ifcMaterialLayerSet, ifcLayerSetDirectionEnum, ifcDirectionSenseEnum, offsetFromReferenceLine);
        }

    }
}
