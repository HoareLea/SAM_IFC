using GeometryGym.Ifc;

namespace SAM.Core.IFC
{
    public static partial class Convert
    {
        public static IfcPropertySet ToIFC(this ParameterSet parameterSet, DatabaseIfc databaseIfc, string sufix = null)
        {
            if(parameterSet == null || databaseIfc == null)
            {
                return null;
            }

            string parameterSetName = parameterSet.Name;
            if (!string.IsNullOrWhiteSpace(sufix))
                parameterSetName = string.Format("{0} [{1}]", parameterSetName, sufix);

            IfcPropertySet result = new IfcPropertySet(databaseIfc, parameterSetName);

            foreach(string name in parameterSet.Names)
            {
                object value = parameterSet.ToObject(name);
                result.SetValue(name, value);
            }

            return result;
        }
    }
}
