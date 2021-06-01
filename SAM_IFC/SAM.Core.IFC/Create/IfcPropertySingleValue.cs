using GeometryGym.Ifc;

namespace SAM.Core.IFC
{
    public static partial class Create
    {
        public static IfcPropertySingleValue IfcPropertySingleValue(this DatabaseIfc databaseIfc, string name, object value)
        {
            if(databaseIfc == null || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            IfcPropertySingleValue result = new IfcPropertySingleValue(databaseIfc, name, value as dynamic);

            return result;
        }


    }
}
