using SAM.Analytical;
using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.SharedBldgElements;

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
                        List<Panel> panels = adjacencyCluster.GetPanels();

                        Dictionary<System.Guid, Dictionary<PanelType, List<IfcBuildingElement>>> dictionary = new Dictionary<System.Guid, Dictionary<PanelType, List<IfcBuildingElement>>>();
                        foreach (Panel panel in panels)
                        {
                            IfcBuildingElement ifcBuildingElement = panel.ToIFC(result);
                            ifcBuilding.AddElement(ifcBuildingElement);

                            System.Guid guid = panel.SAMTypeGuid;
                            if(guid != System.Guid.Empty)
                            {
                                if (!dictionary.TryGetValue(guid, out Dictionary<PanelType, List<IfcBuildingElement>> dictionary_PanelType))
                                {
                                    dictionary_PanelType = new Dictionary<PanelType, List<IfcBuildingElement>>();
                                    dictionary[guid] = dictionary_PanelType;
                                }

                                PanelType panelType = panel.PanelType;

                                if(!dictionary_PanelType.TryGetValue(panelType, out List<IfcBuildingElement> ifcBuildingElements))
                                {
                                    ifcBuildingElements = new List<IfcBuildingElement>();
                                    dictionary_PanelType[panelType] = ifcBuildingElements;
                                }

                                ifcBuildingElements.Add(ifcBuildingElement);
                            }
                        }

                        List<Construction> constructions = adjacencyCluster.GetConstructions();
                        foreach(Construction construction in constructions)
                        {
                            if(!dictionary.TryGetValue(construction.Guid, out Dictionary<PanelType, List<IfcBuildingElement>> dictionary_PanelType))
                            {
                                continue;
                            }

                            foreach(KeyValuePair<PanelType, List<IfcBuildingElement>> keyValuePair in dictionary_PanelType)
                            {
                                IfcBuildingElementType ifcBuildingElementType = construction.ToIFC(result, keyValuePair.Key);
                                if(ifcBuildingElementType == null)
                                {
                                    continue;
                                }

                                //Add Implemenation for IfcBuildingElementType
                                throw new System.NotImplementedException();
                            }
                        }
                    }
                    


                }
            }

            return result;
        }
    }
}
