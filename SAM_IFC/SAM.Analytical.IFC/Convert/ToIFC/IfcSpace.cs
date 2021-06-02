using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSpace ToIFC(this Space space, IfcSpatialStructureElement host, AdjacencyCluster adjacencyCluster = null)
        {
            if (space == null || host == null)
            {
                return null;
            }

            IfcSpace result = new IfcSpace(host, space.Name, 
                Geometry.IFC.Create.IfcLocalPlacement(host.Database, space.Location), 
                Create.IfcProductDefinitionShape(host.Database, space, adjacencyCluster));

            result.LongName = space.Name;
            //result.ObjectType = typeof(Space).Name;
            result.PredefinedType = IfcSpaceTypeEnum.SPACE;

            Core.IFC.Modify.SetIfcPropertySets(result, space);

            return result;
            
        }
    }
}
