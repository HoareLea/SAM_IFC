using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;

namespace SAM.Architectural.IFC
{
    public static partial class Convert
    {
        public static IfcMaterialLayerSetUsage ToIFC_IfcMaterialLayerSetUsage(this IEnumerable<MaterialLayer> materialLayers, Xbim.Common.IModel model)
        {
            if(materialLayers == null || model == null)
            {
                return null;
            }

            IfcMaterialLayerSetUsage result = model.Instances.New<IfcMaterialLayerSetUsage>();
            IfcMaterialLayerSet ifcMaterialLayerSet = materialLayers.ToIFC(model);

            result.ForLayerSet = ifcMaterialLayerSet;
            result.LayerSetDirection = IfcLayerSetDirectionEnum.AXIS2;
            result.DirectionSense = IfcDirectionSenseEnum.NEGATIVE;

            return result;
        }
    }
}
