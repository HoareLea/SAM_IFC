using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcMaterialLayerSetUsage ToIFC_IfcMaterialLayerSetUsage(this IEnumerable<ConstructionLayer> constructionLayers, Xbim.Common.IModel model)
        {
            if(constructionLayers == null || model == null)
            {
                return null;
            }

            IfcMaterialLayerSetUsage result = model.Instances.New<IfcMaterialLayerSetUsage>();
            IfcMaterialLayerSet ifcMaterialLayerSet = constructionLayers.ToIFC(model);

            result.ForLayerSet = ifcMaterialLayerSet;
            result.LayerSetDirection = IfcLayerSetDirectionEnum.AXIS2;
            result.DirectionSense = IfcDirectionSenseEnum.NEGATIVE;

            return result;
        }
    }
}
