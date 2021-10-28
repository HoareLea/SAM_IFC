using System.Collections.Generic;
using Xbim.Ifc4.MaterialResource;

namespace SAM.Architectural.IFC
{
    public static partial class Convert
    {
        public static IfcMaterialLayerSet ToIFC(this IEnumerable<MaterialLayer> materialLayers, Xbim.Common.IModel model)
        {
            if(materialLayers == null || model == null)
            {
                return null;
            }

            IfcMaterialLayerSet result = model.Instances.New<IfcMaterialLayerSet>();

            IEnumerable<IfcMaterial> ifcMaterials = model.Instances.OfType<IfcMaterial>();

            foreach (MaterialLayer materialLayer in materialLayers)
            {
                IfcMaterialLayer ifcMaterialLayer = model.Instances.New<IfcMaterialLayer>();

                //Revit Specific Fix for Thickness less than 0.001m
                double thickness = materialLayer.Thickness;
                if (thickness > 0 && thickness < Core.Tolerance.MacroDistance)
                {
                    thickness = Core.Tolerance.MacroDistance;
                }

                ifcMaterialLayer.LayerThickness = thickness;

                string materialName = materialLayer.Name;

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
