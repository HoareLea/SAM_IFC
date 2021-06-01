using GeometryGym.Ifc;

namespace SAM.Core.IFC
{
    public static partial class Convert
    {
        public static IfcMaterial ToIFC(this IMaterial material, DatabaseIfc databaseIfc)
        {
            if(material == null || databaseIfc == null)
            {
                return null;
            }

            IfcMaterial result = new IfcMaterial(databaseIfc, material.Name);

            return result;
        }
    }
}
