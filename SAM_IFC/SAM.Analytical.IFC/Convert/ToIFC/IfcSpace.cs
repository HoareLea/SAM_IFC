using GeometryGym.Ifc;
using System.Linq;

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

            IfcGeometricRepresentationContext ifcGeometricRepresentationContext = host.Database.Project.Extract<IfcGeometricRepresentationContext>().FirstOrDefault();

            IfcSpace result = new IfcSpace(host, space.Name, 
                Geometry.IFC.Create.IfcLocalPlacement(host.Database, space.Location), 
                Create.IfcProductDefinitionShape(ifcGeometricRepresentationContext, space, adjacencyCluster));

            result.LongName = space.Name;
            //result.ObjectType = typeof(Space).Name;
            result.PredefinedType = IfcSpaceTypeEnum.SPACE;

            Core.IFC.Modify.SetIfcPropertySets(result, space);

            return result;
            
        }
    }
}
