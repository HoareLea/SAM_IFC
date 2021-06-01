using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcWallType ToIFC_IfcWallType(this Construction construction, DatabaseIfc databaseIfc)
        {
            if(construction == null || databaseIfc == null)
            {
                return null;
            }

            IfcWallType result = new IfcWallType(databaseIfc, construction.Name, IfcWallTypeEnum.STANDARD);
            result.SetIfcBuildingElementType(construction);
            Core.IFC.Modify.SetIfcPropertySets(result, construction);

            return result;
        }
    }
}
