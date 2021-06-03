using GeometryGym.Ifc;

namespace SAM.Architectural.IFC
{
    public static partial class Convert
    {
        public static IfcBuildingStorey ToIFC(this Level level, IfcFacility ifcFacility)
        {
            if(level == null || ifcFacility == null)
            {
                return null;
            }

            IfcBuildingStorey result = new IfcBuildingStorey(ifcFacility, level.Name, level.Elevation);
            result.LongName = level.Name;
            result.Guid = level.Guid;
            //result.ObjectType = level.GetType().ToString();
            Core.IFC.Modify.SetIfcPropertySets(result, level);

            return result;
        }
    }
}
