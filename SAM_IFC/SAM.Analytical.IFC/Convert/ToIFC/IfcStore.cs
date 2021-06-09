using SAM.Core;
using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcStore ToIFC(this AnalyticalModel analyticalModel)
        {
            if(analyticalModel == null)
            {
                return null;
            }

            XbimEditorCredentials xbimEditorCredentials = Core.IFC.Query.XbimEditorCredentials();

            IfcStore result = IfcStore.Create(xbimEditorCredentials, Xbim.Common.Step21.XbimSchemaVersion.Ifc4, Xbim.IO.XbimStoreType.InMemoryModel);

            IfcProject ifcProject = null;
            using (ITransaction transaction = result.BeginTransaction("Create Project"))
            {
                ifcProject = analyticalModel.ToIFC(result);

                IfcGeometricRepresentationContext ifcGeometricRepresentationContext = result.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();

                Geometry.IFC.Create.IfcGeometricRepresentationSubContext(ifcGeometricRepresentationContext, Geometry.IFC.IfcDefaultContextIdentifier.Axis);
                Geometry.IFC.Create.IfcGeometricRepresentationSubContext(ifcGeometricRepresentationContext, Geometry.IFC.IfcDefaultContextIdentifier.Body);

                transaction.Commit();
            }

            using (ITransaction transaction = result.BeginTransaction("Create Materials"))
            {
                Dictionary<string, IfcMaterial> dictionary_IfcMaterial = new Dictionary<string, IfcMaterial>();
                List<IMaterial> materials = analyticalModel.MaterialLibrary?.GetMaterials();
                if (materials != null)
                {
                    foreach (IMaterial material in materials)
                    {
                        IfcMaterial ifcMaterial = Core.IFC.Convert.ToIFC(material, result);
                    }
                }

                transaction.Commit();
            }

            ifcProject = result.Instances.OfType<IfcProject>().FirstOrDefault();
            if(ifcProject != null)
            {
                IfcBuilding ifcBuilding = null;
                using (ITransaction transaction = result.BeginTransaction("Create Building"))
                {
                    ifcBuilding = analyticalModel.ToIFC_IfcBuilding(result);
                    ifcProject.AddBuilding(ifcBuilding);

                    transaction.Commit();
                }

                ifcBuilding = result.Instances.OfType<IfcBuilding>().FirstOrDefault();
                if(ifcBuilding != null)
                {
                    AdjacencyCluster adjacencyCluster = analyticalModel.AdjacencyCluster;
                    if(adjacencyCluster != null)
                    {
                        Dictionary<System.Guid, Dictionary<PanelType, List<IfcBuildingElement>>> dictionary = new Dictionary<System.Guid, Dictionary<PanelType, List<IfcBuildingElement>>>();

                        List<Panel> panels = adjacencyCluster.GetPanels();
                        if(panels != null && panels.Count != 0)
                        {
                            Dictionary<Architectural.Level, List<Panel>> dictionary_Levels = Analytical.Query.LevelsDictionary(panels);
                            using (ITransaction transaction = result.BeginTransaction("Create Building Elements"))
                            {
                                IfcRelAggregates ifcRelAggregates = result.Instances.New<IfcRelAggregates>();
                                ifcRelAggregates.RelatingObject = ifcBuilding;

                                List<Architectural.Level> levels = dictionary_Levels.Keys.ToList();
                                levels.Sort((x, y) => x.Elevation.CompareTo(y.Elevation));

                                foreach (Architectural.Level level in levels)
                                {
                                    IfcBuildingStorey ifcBuildingStorey = Architectural.IFC.Convert.ToIFC(level, result);
                                    ifcRelAggregates.RelatedObjects.Add(ifcBuildingStorey);

                                    foreach (Panel panel in dictionary_Levels[level])
                                    {
                                        IfcBuildingElement ifcBuildingElement = panel.ToIFC(result);
                                        if(ifcBuildingElement == null)
                                        {
                                            continue;
                                        }

                                        ifcBuildingStorey.AddElement(ifcBuildingElement);

                                        ifcBuildingElement.SetIsExternal(panel, adjacencyCluster);

                                        System.Guid guid = panel.SAMTypeGuid;
                                        if (guid != System.Guid.Empty)
                                        {
                                            if (!dictionary.TryGetValue(guid, out Dictionary<PanelType, List<IfcBuildingElement>> dictionary_PanelType))
                                            {
                                                dictionary_PanelType = new Dictionary<PanelType, List<IfcBuildingElement>>();
                                                dictionary[guid] = dictionary_PanelType;
                                            }

                                            PanelType panelType = panel.PanelType;

                                            if (!dictionary_PanelType.TryGetValue(panelType, out List<IfcBuildingElement> ifcBuildingElements))
                                            {
                                                ifcBuildingElements = new List<IfcBuildingElement>();
                                                dictionary_PanelType[panelType] = ifcBuildingElements;
                                            }

                                            ifcBuildingElements.Add(ifcBuildingElement);
                                        }
                                    }
                                }

                                transaction.Commit();
                            }
                        }

                        List<Construction> constructions = adjacencyCluster.GetConstructions();
                        if(constructions != null && constructions.Count != 0)
                        {
                            using (ITransaction transaction = result.BeginTransaction("Create Building Element Types"))
                            {

                                foreach (Construction construction in constructions)
                                {
                                    if (!dictionary.TryGetValue(construction.Guid, out Dictionary<PanelType, List<IfcBuildingElement>> dictionary_PanelType))
                                    {
                                        continue;
                                    }

                                    foreach (KeyValuePair<PanelType, List<IfcBuildingElement>> keyValuePair in dictionary_PanelType)
                                    {
                                        PanelGroup panelGroup = keyValuePair.Key.PanelGroup();

                                        IfcMaterialLayerSet ifcMaterialLayerSet = null;
                                        IfcRelAssociatesMaterial ifcRelAssociatesMaterial_Type = null;

                                        List<ConstructionLayer> constructionLayers = construction.ConstructionLayers;
                                        if(constructionLayers != null)
                                        {
                                            if (panelGroup == PanelGroup.Roof)
                                            {
                                                constructionLayers.Reverse();
                                            }

                                            ifcMaterialLayerSet = constructionLayers.ToIFC(result);
                                        }

                                        if(ifcMaterialLayerSet != null)
                                        {
                                            ifcMaterialLayerSet.LayerSetName = construction.Name;

                                            ifcRelAssociatesMaterial_Type = result.Instances.New<IfcRelAssociatesMaterial>();
                                            ifcRelAssociatesMaterial_Type.RelatingMaterial = ifcMaterialLayerSet;
                                        }


                                        IfcBuildingElementType ifcBuildingElementType = construction.ToIFC(result, keyValuePair.Key);
                                        if (ifcBuildingElementType == null)
                                        {
                                            continue;
                                        }

                                        ifcBuildingElementType.SetIsExternal(keyValuePair.Key);

                                        IfcRelDefinesByType ifcRelDefinesByType = result.Instances.New<IfcRelDefinesByType>();
                                        ifcRelDefinesByType.RelatingType = ifcBuildingElementType;
                                        ifcRelDefinesByType.Name = ifcBuildingElementType.Name;

                                        IfcRelAssociatesMaterial ifcRelAssociatesMaterial_Instance = null;


                                        if (ifcRelAssociatesMaterial_Type != null)
                                        {
                                            ifcRelAssociatesMaterial_Type.RelatedObjects.Add(ifcBuildingElementType);

                                            ifcRelAssociatesMaterial_Instance = result.Instances.New<IfcRelAssociatesMaterial>();
                                            IfcMaterialLayerSetUsage ifcMaterialLayerSetUsage = Create.IfcMaterialLayerSetUsage(ifcMaterialLayerSet, panelGroup);
                                            ifcRelAssociatesMaterial_Instance.RelatingMaterial = ifcMaterialLayerSetUsage;
                                        }



                                        foreach (IfcBuildingElement ifcBuildingElement in keyValuePair.Value)
                                        {
                                            ifcRelDefinesByType.RelatedObjects.Add(ifcBuildingElement);

                                            if(ifcRelAssociatesMaterial_Instance != null)
                                            {
                                                ifcRelAssociatesMaterial_Instance.RelatedObjects.Add(ifcBuildingElement);
                                            }
                                        }
                                    }
                                }

                                transaction.Commit();
                            }
                        }

                        List<Space> spaces = adjacencyCluster.GetSpaces();
                        if (spaces != null && spaces.Count != 0)
                        {
                            using (ITransaction transaction = result.BeginTransaction("Create Spaces"))
                            {
                                List<IfcBuildingStorey> ifcBuildingStoreys = result.Instances.OfType<IfcBuildingStorey>()?.ToList();
                                ifcBuildingStoreys?.Sort((x, y) => ((double)x.Elevation).CompareTo((double)y.Elevation));

                                Dictionary<IfcBuildingStorey, IfcRelAggregates> dictionary_IfcRelAggregates = new Dictionary<IfcBuildingStorey, IfcRelAggregates>();

                                foreach (Space space in spaces)
                                {
                                    IfcSpace ifcSpace = space?.ToIFC(result, adjacencyCluster);
                                    if (ifcSpace == null)
                                    {
                                        continue;
                                    }

                                    double elevation = space.Location.Z;

                                    if (ifcBuildingStoreys == null || ifcBuildingStoreys.Count == 0)
                                    {
                                        ifcBuilding.AddElement(ifcSpace);
                                    }
                                    else
                                    {
                                        IfcBuildingStorey ifcBuildingStorey = null;
                                        if (ifcBuildingStoreys.Count == 1)
                                        {
                                            ifcBuildingStorey = ifcBuildingStoreys.First();
                                        }
                                        else
                                        {
                                            for (int i = 1; i < ifcBuildingStoreys.Count; i++)
                                            {
                                                if (ifcBuildingStoreys[i].Elevation > elevation)
                                                {
                                                    ifcBuildingStorey = ifcBuildingStoreys[i - 1];
                                                    break;
                                                }
                                            }
                                        }

                                        if (ifcBuildingStorey == null)
                                            ifcBuildingStorey = ifcBuildingStoreys.Last();

                                        if (!dictionary_IfcRelAggregates.TryGetValue(ifcBuildingStorey, out IfcRelAggregates ifcRelAggregates) || ifcRelAggregates == null)
                                        {
                                            ifcRelAggregates = result.Instances.New<IfcRelAggregates>();
                                            ifcRelAggregates.RelatingObject = ifcBuildingStorey;
                                        }

                                        ifcRelAggregates.RelatedObjects.Add(ifcSpace);
                                    }


                                }

                                transaction.Commit();
                            }

                        }
                    }

                }
            }

            return result;
        }
    }
}
