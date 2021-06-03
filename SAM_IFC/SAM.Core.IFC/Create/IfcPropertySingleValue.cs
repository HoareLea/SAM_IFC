using Xbim.Ifc4.PropertyResource;

namespace SAM.Core.IFC
{
    public static partial class Create
    {
        public static IfcPropertySingleValue IfcPropertySingleValue(this Xbim.Common.IModel model, string name, object value)
        {
            if(model == null || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }



            IfcPropertySingleValue result = model.Instances.New<IfcPropertySingleValue>();
            result.Name = name;

            return result;
        }


    }
}
