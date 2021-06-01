using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Create
    {
        public static IfcMaterialLayerSetUsage IfcMaterialLayerSetUsage(this IfcMaterialLayerSet ifcMaterialLayerSet)
        {
            if(ifcMaterialLayerSet == null)
            {
                return null;
            }

            IfcMaterialLayerSetUsage result = new IfcMaterialLayerSetUsage(ifcMaterialLayerSet, IfcLayerSetDirectionEnum.AXIS2, IfcDirectionSenseEnum.NEGATIVE, 0);
            return result;
        }

    }
}
