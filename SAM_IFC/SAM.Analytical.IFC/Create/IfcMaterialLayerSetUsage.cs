using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;

namespace SAM.Analytical.IFC
{
    public static partial class Create
    {
        public static IfcMaterialLayerSetUsage IfcMaterialLayerSetUsage(this IfcMaterialLayerSet ifcMaterialLayerSet)
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
            result.LayerSetDirection = IfcLayerSetDirectionEnum.AXIS2;
            result.DirectionSense = IfcDirectionSenseEnum.NEGATIVE;

            return result;
        }

    }
}
