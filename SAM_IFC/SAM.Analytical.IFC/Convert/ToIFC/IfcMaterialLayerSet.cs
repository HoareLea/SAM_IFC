using System.Collections.Generic;
using Xbim.Ifc4.MaterialResource;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcMaterialLayerSet ToIFC(this IEnumerable<ConstructionLayer> constructionLayers, Xbim.Common.IModel model)
        {
            if(constructionLayers == null || model == null)
            {
                return null;
            }

            IfcMaterialLayerSet result = model.Instances.New<IfcMaterialLayerSet>();

            IEnumerable<IfcMaterial> ifcMaterials = model.Instances.OfType<IfcMaterial>();

            foreach (ConstructionLayer constructionLayer in constructionLayers)
            {
                IfcMaterialLayer ifcMaterialLayer = model.Instances.New<IfcMaterialLayer>();

                //Revit Specific Fix for Thickness less than 0.001m
                double thickness = constructionLayer.Thickness;
                if (thickness > 0 && thickness < Core.Tolerance.MacroDistance)
                {
                    thickness = Core.Tolerance.MacroDistance;
                }

                ifcMaterialLayer.LayerThickness = thickness;

                string materialName = constructionLayer.Name;

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

                result.MaterialLayers.Add(ifcMaterialLayer);
            }

            return result;
        }
    }
}
