using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcRoofType ToIFC_IfcRoofType(this Construction construction, DatabaseIfc databaseIfc)
        {
            if(construction == null || databaseIfc == null)
            {
                return null;
            }

            IfcRoofType result = new IfcRoofType(databaseIfc, construction.Name, IfcRoofTypeEnum.NOTDEFINED);
            result.SetIfcBuildingElementType(construction);
            //Core.IFC.Modify.SetIfcPropertySets(result, construction);

            return result;
        }
    }
}
