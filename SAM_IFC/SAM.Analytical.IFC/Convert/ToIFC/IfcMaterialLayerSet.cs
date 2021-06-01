using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcMaterialLayerSet ToIFC(this IEnumerable<ConstructionLayer> constructionLayers, DatabaseIfc databaseIfc)
        {
            if(constructionLayers == null || databaseIfc == null)
            {
                return null;
            }

            IEnumerable<IfcMaterial> ifcMaterials = databaseIfc.Project?.Extract<IfcMaterial>();

            List<IfcMaterialLayer> ifcMaterialLayers = new List<IfcMaterialLayer>();
            foreach (ConstructionLayer constructionLayer in constructionLayers)
            {
                string materialName = constructionLayer.Name;

                IfcMaterialLayer ifcMaterialLayer = new IfcMaterialLayer(databaseIfc, constructionLayer.Thickness, materialName);

                if (ifcMaterials != null && !string.IsNullOrWhiteSpace(materialName))
                {
                    foreach (IfcMaterial ifcMaterial in ifcMaterials)
                    {
                        if (materialName.Equals(ifcMaterial?.Name))
                        {
                            ifcMaterialLayer.Material = ifcMaterial;
                            break;
                        }
                    }
                }


                ifcMaterialLayers.Add(ifcMaterialLayer);
            }

            IfcMaterialLayerSet result = new IfcMaterialLayerSet(ifcMaterialLayers, string.Empty);
            return result;
        }
    }
}
