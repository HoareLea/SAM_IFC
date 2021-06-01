using GeometryGym.Ifc;

namespace SAM.Core.IFC
{
    public static partial class Modify
    {
        public static bool SetValue(this IfcPropertySet ifcPropertySet, string name, object value)
        {
            if(ifcPropertySet == null || string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            DatabaseIfc databaseIfc = ifcPropertySet.Database;
            if(databaseIfc == null)
            {
                return false;
            }

            IfcPropertySingleValue ifcPropertySingleValue = Create.IfcPropertySingleValue(databaseIfc, name, value);
            if(ifcPropertySingleValue == null)
            {
                return false;
            }

            ifcPropertySet.HasProperties.Add(name, ifcPropertySingleValue);

            return true;
        }

        public static bool SetValue(this IfcPropertySingleValue ifcPropertySingleValue, object value)
        {
            if(ifcPropertySingleValue == null || value == null)
            {
                return false;
            }

            if(value is string)
            {
                return SetValue(ifcPropertySingleValue, (string)value);
            }
            else if(value is double)
            {
                return SetValue(ifcPropertySingleValue, (double)value);
            }
            else if (value is int)
            {
                return SetValue(ifcPropertySingleValue, (int)value);
            }
            else if (value is bool)
            {
                return SetValue(ifcPropertySingleValue, (bool)value);
            }

            return false;
        }

        public static bool SetValue(this IfcPropertySingleValue ifcPropertySingleValue, string value)
        {
            if (ifcPropertySingleValue == null || value == null)
            {
                return false;
            }

            ifcPropertySingleValue.NominalValue = Create.IfcText(value);
            return true;
        }

        public static bool SetValue(this IfcPropertySingleValue ifcPropertySingleValue, double value)
        {
            if (ifcPropertySingleValue == null)
            {
                return false;
            }

            ifcPropertySingleValue.NominalValue = Create.IfcReal(value);
            return true;
        }

        public static bool SetValue(this IfcPropertySingleValue ifcPropertySingleValue, int value)
        {
            if (ifcPropertySingleValue == null)
            {
                return false;
            }

            ifcPropertySingleValue.NominalValue = Create.IfcInteger(value);
            return true;
        }

        public static bool SetValue(this IfcPropertySingleValue ifcPropertySingleValue, bool value)
        {
            if (ifcPropertySingleValue == null)
            {
                return false;
            }

            ifcPropertySingleValue.NominalValue = Create.IfcBoolean(value);
            return true;
        }
    }
}
