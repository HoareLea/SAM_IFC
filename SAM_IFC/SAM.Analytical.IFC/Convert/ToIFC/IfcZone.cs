using System.Collections.Generic;
using System.Linq;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.UtilityResource;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcZone ToIFC(this Zone zone, Xbim.Common.IModel model, AdjacencyCluster adjacencyCluster = null)
        {
            if (zone == null || model == null)
            {
                return null;
            }

            IfcZone result = model.Instances.New<IfcZone>();
            result.Name = zone.Name;
            result.ObjectType = typeof(Zone).Name;

            Core.IFC.Modify.SetIfcPropertySets(result, zone);

            if(adjacencyCluster != null)
            {
                List<Space> spaces = adjacencyCluster.GetSpaces(zone);
                if(spaces != null && spaces.Count != 0)
                {
                    List<IfcSpace> ifcSpaces = model.Instances.OfType<IfcSpace>()?.ToList();
                    if(ifcSpaces != null && ifcSpaces.Count() != 0)
                    {
                        IfcRelAssignsToGroup ifcRelAssignsToGroup = model.Instances.New<IfcRelAssignsToGroup>();
                        ifcRelAssignsToGroup.RelatingGroup = result;

                        foreach(Space space in spaces)
                        {
                            IfcGloballyUniqueId ifcGloballyUniqueId = space.Guid;

                            IfcSpace ifcSpace = ifcSpaces.Find(x => x.GlobalId == ifcGloballyUniqueId);
                            if(ifcSpace != null)
                            {
                                ifcRelAssignsToGroup.RelatedObjects.Add(ifcSpace);
                            }
                        }
                    }
                }
            }

            return result;
            
        }
    }
}
