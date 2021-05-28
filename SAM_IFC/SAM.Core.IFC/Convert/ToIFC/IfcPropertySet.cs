using Xbim.Ifc4.Kernel;

namespace SAM.Core.IFC
{
    public static partial class Convert
    {
        public static IfcPropertySet ToIFC(this ParameterSet parameterSet, Xbim.Common.IModel model)
        {
            if(parameterSet == null || model == null)
            {
                return null;
            }

            IfcPropertySet result = model.Instances.New<IfcPropertySet>();
            result.Name = parameterSet.Name;

            foreach(string name in parameterSet.Names)
            {
                object value = parameterSet.ToObject(name);
                result.SetValue(name, value);
            }

            return result;
        }
    }
}
