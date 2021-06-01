using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcCurtainWallType ToIFC_IfcCurtainWallType(this Construction construction, DatabaseIfc databaseIfc)
        {
            if(construction == null || databaseIfc == null)
            {
                return null;
            }

            IfcCurtainWallType result = new IfcCurtainWallType(databaseIfc, construction.Name, IfcCurtainWallTypeEnum.NOTDEFINED);
            result.SetIfcBuildingElementType(construction);
            Core.IFC.Modify.SetIfcPropertySets(result, construction);

            return result;
        }
    }
}
