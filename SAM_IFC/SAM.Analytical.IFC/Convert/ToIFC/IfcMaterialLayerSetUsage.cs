using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcMaterialLayerSetUsage ToIFC_IfcMaterialLayerSetUsage(this IEnumerable<ConstructionLayer> constructionLayers, DatabaseIfc databaseIfc)
        {
            if(constructionLayers == null || databaseIfc == null)
            {
                return null;
            }

            IfcMaterialLayerSetUsage result = new IfcMaterialLayerSetUsage(constructionLayers.ToIFC(databaseIfc), IfcLayerSetDirectionEnum.AXIS2, IfcDirectionSenseEnum.NEGATIVE, 0);
            return result;
        }
    }
}
