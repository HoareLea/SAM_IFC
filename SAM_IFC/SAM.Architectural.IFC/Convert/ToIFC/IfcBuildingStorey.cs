using Xbim.Ifc4.ProductExtension;

namespace SAM.Architectural.IFC
{
    public static partial class Convert
    {
        public static IfcBuildingStorey ToIFC(this Level level, Xbim.Common.IModel model)
        {
            if(level == null || model == null)
            {
                return null;
            }

            IfcBuildingStorey result = model.Instances.New<IfcBuildingStorey>();
            result.Name = level.Name;
            result.LongName = level.Name;
            result.GlobalId = level.Guid;
            //result.ObjectType = typeof(Level).ToString();
            result.Elevation = level.Elevation;
            Core.IFC.Modify.SetIfcPropertySets(result, level);

            return result;
        }
    }
}
