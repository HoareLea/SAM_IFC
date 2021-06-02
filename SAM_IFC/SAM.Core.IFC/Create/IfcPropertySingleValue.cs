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

            IfcPropertySingleValue result = null;


            if (value is bool)
            {
                return new IfcPropertySingleValue(databaseIfc, name, (bool)value);
            }

            if(value is double)
            {
                return new IfcPropertySingleValue(databaseIfc, name, (double)value);
            }
           
            if (value is int)
            {
                return new IfcPropertySingleValue(databaseIfc, name, (int)value);
            }
            
            if (value is string)
            {
                return new IfcPropertySingleValue(databaseIfc, name, (string)value);
            }

            if(value != null)
            {
                return new IfcPropertySingleValue(databaseIfc, name, value.ToString());
            }

            return result;
        }


    }
}
