using System.Collections.Generic;
using System.Linq;
using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcMaterialLayerSet ToIFC(this IEnumerable<ConstructionLayer> constructionLayers, DatabaseIfc databaseIfc, IEnumerable<IfcMaterial> ifcMaterials = null)
        {
            if(constructionLayers == null || databaseIfc == null)
            {
                return null;
            }

            List<IfcMaterial> ifcMaterials_Temp = null;
            if (ifcMaterials != null)
            {
                ifcMaterials_Temp = new List<IfcMaterial>(ifcMaterials);
            }
            else
            {
                ifcMaterials_Temp = new List<IfcMaterial>(databaseIfc.OfType<IfcMaterial>());
            }

            List<IfcMaterialLayer> ifcMaterialLayers = new List<IfcMaterialLayer>();
            foreach (ConstructionLayer constructionLayer in constructionLayers)
            {
                string materialName = constructionLayer.Name;

                IfcMaterialLayer ifcMaterialLayer = new IfcMaterialLayer(databaseIfc, constructionLayer.Thickness, materialName);

                if (ifcMaterials_Temp != null && !string.IsNullOrWhiteSpace(materialName))
                {
                    foreach (IfcMaterial ifcMaterial in ifcMaterials_Temp)
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
