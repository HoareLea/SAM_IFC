using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSlabType ToIFC_IfcSlabType(this Construction construction, DatabaseIfc databaseIfc)
        {
            if(construction == null || databaseIfc == null)
            {
                return null;
            }

            IfcSlabType result = new IfcSlabType(databaseIfc, construction.Name, IfcSlabTypeEnum.FLOOR);
            result.SetIfcBuildingElementType(construction);
            //Core.IFC.Modify.SetIfcPropertySets(result, construction);

            return result;
        }
    }
}
