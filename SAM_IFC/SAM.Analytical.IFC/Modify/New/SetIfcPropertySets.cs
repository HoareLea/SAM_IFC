using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static void SetIfcPropertySets(this IfcWall ifcWall, Wall wall, ArchitecturalModel architecturalModel)
        {
            if(ifcWall == null || wall == null || architecturalModel == null)
            {
                return;
            }
            
            Core.IFC.Modify.SetIfcPropertySets(ifcWall, wall);

            ifcWall.SetPropertySingleValue("Pset_WallCommon", "IsExternal", new IfcBoolean(architecturalModel.External(wall)));

        }
    }
}
