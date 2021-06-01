using GeometryGym.Ifc;

namespace SAM.Core.IFC
{
    public static partial class Convert
    {
        public static IfcPropertySet ToIFC(this ParameterSet parameterSet, DatabaseIfc databaseIfc)
        {
            if(parameterSet == null || databaseIfc == null)
            {
                return null;
            }

            IfcPropertySet result = new IfcPropertySet(databaseIfc, parameterSet.Name);

            foreach(string name in parameterSet.Names)
            {
                object value = parameterSet.ToObject(name);
                result.SetValue(name, value);
            }

            return result;
        }
    }
}
