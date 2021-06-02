using SAM.Core;
using System.Collections.Generic;
using System.Linq;
using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static DatabaseIfc ToIFC(this AnalyticalModel analyticalModel)
        {
            if(analyticalModel == null)
            {
                return null;
            }

            DatabaseIfc result = new DatabaseIfc(ModelView.Ifc2x3Coordination);

            IfcBuilding ifcBuilding = analyticalModel.ToIFC_IfcBuilding(result);

            IfcProject ifcProject = analyticalModel.ToIFC(ifcBuilding);

            Dictionary<string, IfcMaterial> dictionary_IfcMaterial = new Dictionary<string, IfcMaterial>();
            List<IMaterial> materials = analyticalModel.MaterialLibrary?.GetMaterials();
            if (materials != null)
            {
                foreach (IMaterial material in materials)
                {
                    IfcMaterial ifcMaterial = Core.IFC.Convert.ToIFC(material, result);
                }
            }

            AdjacencyCluster adjacencyCluster = analyticalModel.AdjacencyCluster;
            if (adjacencyCluster != null)
            {
                Dictionary<System.Guid, Dictionary<PanelType, List<IfcBuiltElement>>> dictionary = new Dictionary<System.Guid, Dictionary<PanelType, List<IfcBuiltElement>>>();

                Dictionary<Architectural.Level, List<Panel>> dictionary_Levels = Query.LevelsDictionary(adjacencyCluster.GetPanels());
                

                List<Architectural.Level> levels = dictionary_Levels.Keys.ToList();
                levels.Sort((x, y) => x.Elevation.CompareTo(y.Elevation));

                List<IfcBuildingStorey> IfcBuildingStoreys = new List<IfcBuildingStorey>();
                foreach (Architectural.Level level in levels)
                {
                    IfcBuildingStorey ifcBuildingStorey = Architectural.IFC.Convert.ToIFC(level, ifcBuilding);
                    IfcBuildingStoreys.Add(ifcBuildingStorey);

                    foreach (Panel panel in dictionary_Levels[level])
                    {
                        IfcBuiltElement ifcBuiltElement = panel.ToIFC(ifcBuildingStorey);
                        if(ifcBuiltElement == null)
                        {
                            continue;
                        }

                        ifcBuildingStorey.AddElement(ifcBuiltElement);

                        System.Guid guid = panel.SAMTypeGuid;
                        if (guid != System.Guid.Empty)
                        {
                            if (!dictionary.TryGetValue(guid, out Dictionary<PanelType, List<IfcBuiltElement>> dictionary_PanelType))
                            {
                                dictionary_PanelType = new Dictionary<PanelType, List<IfcBuiltElement>>();
                                dictionary[guid] = dictionary_PanelType;
                            }

                            PanelType panelType = panel.PanelType;

                            if (!dictionary_PanelType.TryGetValue(panelType, out List<IfcBuiltElement> IfcBuiltElements))
                            {
                                IfcBuiltElements = new List<IfcBuiltElement>();
                                dictionary_PanelType[panelType] = IfcBuiltElements;
                            }

                            IfcBuiltElements.Add(ifcBuiltElement);
                        }
                    }
                }

                IfcRelAggregates ifcRelAggregates = new IfcRelAggregates(ifcBuilding, IfcBuildingStoreys);

                List<Construction> constructions = adjacencyCluster.GetConstructions();
                foreach (Construction construction in constructions)
                {
                    if (!dictionary.TryGetValue(construction.Guid, out Dictionary<PanelType, List<IfcBuiltElement>> dictionary_PanelType))
                    {
                        continue;
                    }

                    IfcMaterialLayerSet ifcMaterialLayerSet = construction.ConstructionLayers.ToIFC(result);

                    IfcRelAssociatesMaterial ifcRelAssociatesMaterial_Type = new IfcRelAssociatesMaterial(ifcMaterialLayerSet);

                    foreach (KeyValuePair<PanelType, List<IfcBuiltElement>> keyValuePair in dictionary_PanelType)
                    {
                        IfcBuiltElementType ifcBuildingElementType = construction.ToIFC(result, keyValuePair.Key);
                        if (ifcBuildingElementType == null)
                        {
                            continue;
                        }

                        if (ifcRelAssociatesMaterial_Type != null)
                        {
                            ifcRelAssociatesMaterial_Type.RelatedObjects.Add(ifcBuildingElementType);
                        }

                        IfcRelDefinesByType ifcRelDefinesByType = new IfcRelDefinesByType(ifcBuildingElementType);
                        ifcRelDefinesByType.Name = ifcBuildingElementType.Name;

                        IfcRelAssociatesMaterial ifcRelAssociatesMaterial_Instance = new IfcRelAssociatesMaterial(Create.IfcMaterialLayerSetUsage(ifcMaterialLayerSet));

                        foreach (IfcBuiltElement ifcBuiltElement in keyValuePair.Value)
                        {
                            ifcRelDefinesByType.RelatedObjects.Add(ifcBuiltElement);
                            ifcRelAssociatesMaterial_Instance.RelatedObjects.Add(ifcBuiltElement);
                        }
                    }
                }

                List<Space> spaces = adjacencyCluster.GetSpaces();
                foreach (Space space in spaces)
                {
                    IfcSpace ifcSpace = space?.ToIFC(ifcBuilding, adjacencyCluster);
                    if (ifcSpace == null)
                    {
                        continue;
                    }

                    ifcBuilding.AddElement(ifcSpace);
                }

            }

            return result;
        }
    }
}
