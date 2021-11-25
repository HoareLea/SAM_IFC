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
        public static IfcStore ToIFC(this BuildingModel buildingModel)
        {
            if(buildingModel == null)
            {
                return null;
            }

            XbimEditorCredentials xbimEditorCredentials = Core.IFC.Query.XbimEditorCredentials();

            IfcStore result = IfcStore.Create(xbimEditorCredentials, Xbim.Common.Step21.XbimSchemaVersion.Ifc4, Xbim.IO.XbimStoreType.InMemoryModel);

            IfcProject ifcProject = null;
            using (ITransaction transaction = result.BeginTransaction("Create Project"))
            {
                ifcProject = buildingModel.ToIFC(result);

                IfcGeometricRepresentationContext ifcGeometricRepresentationContext = result.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();

                Geometry.IFC.Create.IfcGeometricRepresentationSubContext(ifcGeometricRepresentationContext, Geometry.IFC.IfcDefaultContextIdentifier.Axis);
                Geometry.IFC.Create.IfcGeometricRepresentationSubContext(ifcGeometricRepresentationContext, Geometry.IFC.IfcDefaultContextIdentifier.Body);

                transaction.Commit();
            }

            using (ITransaction transaction = result.BeginTransaction("Create Materials"))
            {
                Dictionary<string, IfcMaterial> dictionary_IfcMaterial = new Dictionary<string, IfcMaterial>();
                List<IMaterial> materials = buildingModel.GetMaterials();
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
                    ifcBuilding = buildingModel.ToIFC_IfcBuilding(result);
                    ifcProject.AddBuilding(ifcBuilding);

                    transaction.Commit();
                }

                ifcBuilding = result.Instances.OfType<IfcBuilding>().FirstOrDefault();
                if(ifcBuilding != null)
                {
                    Dictionary<System.Guid, Dictionary<PartitionAnalyticalType, List<IfcBuildingElement>>> dictionary = new Dictionary<System.Guid, Dictionary<PartitionAnalyticalType, List<IfcBuildingElement>>>();

                    List<IPartition> partitions = buildingModel.GetPartitions();
                    if (partitions != null && partitions.Count != 0)
                    {
                        Dictionary<Architectural.Level, List<IPartition>> dictionary_Levels = Architectural.Query.LevelsDictionary(partitions);
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

                                foreach (IPartition partition in dictionary_Levels[level])
                                {
                                    IfcBuildingElement ifcBuildingElement = partition.ToIFC(result, buildingModel);
                                    if (ifcBuildingElement == null)
                                    {
                                        continue;
                                    }

                                    ifcBuildingStorey.AddElement(ifcBuildingElement);

                                    ifcBuildingElement.SetIsExternal(partition, buildingModel);

                                    if(partition is IHostPartition)
                                    {
                                        System.Guid guid = ((IHostPartition)partition).Type().Guid;
                                        if (guid != System.Guid.Empty)
                                        {
                                            if (!dictionary.TryGetValue(guid, out Dictionary<PartitionAnalyticalType, List<IfcBuildingElement>> dictionary_PartitionAnalyticalType))
                                            {
                                                dictionary_PartitionAnalyticalType = new Dictionary<PartitionAnalyticalType, List<IfcBuildingElement>>();
                                                dictionary[guid] = dictionary_PartitionAnalyticalType;
                                            }

                                            PartitionAnalyticalType partitionAnalyticalType = buildingModel.PartitionAnalyticalType(partition);

                                            if (!dictionary_PartitionAnalyticalType.TryGetValue(partitionAnalyticalType, out List<IfcBuildingElement> ifcBuildingElements))
                                            {
                                                ifcBuildingElements = new List<IfcBuildingElement>();
                                                dictionary_PartitionAnalyticalType[partitionAnalyticalType] = ifcBuildingElements;
                                            }

                                            ifcBuildingElements.Add(ifcBuildingElement);
                                        }
                                    }


                                }
                            }

                            transaction.Commit();
                        }
                    }

                    List<HostPartitionType> hostPartitionTypes = buildingModel.GetHostPartitionTypes();
                    if (hostPartitionTypes != null && hostPartitionTypes.Count != 0)
                    {
                        using (ITransaction transaction = result.BeginTransaction("Create Building Element Types"))
                        {

                            foreach (HostPartitionType hostPartitionType in hostPartitionTypes)
                            {
                                if (!dictionary.TryGetValue(hostPartitionType.Guid, out Dictionary<PartitionAnalyticalType, List<IfcBuildingElement>> dictionary_PartitionAnalyticalType))
                                {
                                    continue;
                                }

                                foreach (KeyValuePair<PartitionAnalyticalType, List<IfcBuildingElement>> keyValuePair in dictionary_PartitionAnalyticalType)
                                {
                                    IfcMaterialLayerSet ifcMaterialLayerSet = null;
                                    IfcRelAssociatesMaterial ifcRelAssociatesMaterial_Type = null;

                                    List<Architectural.MaterialLayer> materialLayers = hostPartitionType.MaterialLayers;
                                    if (materialLayers != null)
                                    {
                                        if(keyValuePair.Key == PartitionAnalyticalType.Roof)
                                        {
                                            materialLayers.Reverse();
                                        }

                                        ifcMaterialLayerSet = Architectural.IFC.Convert.ToIFC(materialLayers, result);
                                    }

                                    if (ifcMaterialLayerSet != null)
                                    {
                                        ifcMaterialLayerSet.LayerSetName = hostPartitionType.Name;

                                        ifcRelAssociatesMaterial_Type = result.Instances.New<IfcRelAssociatesMaterial>();
                                        ifcRelAssociatesMaterial_Type.RelatingMaterial = ifcMaterialLayerSet;
                                    }


                                    IfcBuildingElementType ifcBuildingElementType = hostPartitionType.ToIFC(result, buildingModel);
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
                                        IfcMaterialLayerSetUsage ifcMaterialLayerSetUsage = Create.IfcMaterialLayerSetUsage(ifcMaterialLayerSet, Analytical.Query.HostPartitionCategory(hostPartitionType));
                                        ifcRelAssociatesMaterial_Instance.RelatingMaterial = ifcMaterialLayerSetUsage;
                                    }



                                    foreach (IfcBuildingElement ifcBuildingElement in keyValuePair.Value)
                                    {
                                        ifcRelDefinesByType.RelatedObjects.Add(ifcBuildingElement);

                                        if (ifcRelAssociatesMaterial_Instance != null)
                                        {
                                            ifcRelAssociatesMaterial_Instance.RelatedObjects.Add(ifcBuildingElement);
                                        }
                                    }
                                }
                            }

                            transaction.Commit();
                        }
                    }

                    List<Space> spaces = buildingModel.GetSpaces();
                    if (spaces != null && spaces.Count != 0)
                    {
                        using (ITransaction transaction = result.BeginTransaction("Create Spaces"))
                        {
                            List<IfcBuildingStorey> ifcBuildingStoreys = result.Instances.OfType<IfcBuildingStorey>()?.ToList();
                            ifcBuildingStoreys?.Sort((x, y) => ((double)x.Elevation).CompareTo((double)y.Elevation));

                            Dictionary<IfcBuildingStorey, IfcRelAggregates> dictionary_IfcRelAggregates = new Dictionary<IfcBuildingStorey, IfcRelAggregates>();

                            foreach (Space space in spaces)
                            {
                                IfcSpace ifcSpace = space?.ToIFC(result, buildingModel);
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

            return result;
        }
    }
}
