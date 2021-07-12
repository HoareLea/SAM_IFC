using System.Collections.Generic;
using System.Linq;
using Xbim.Properties;

namespace SAM.Core.IFC
{
    public static partial class Query
    {
        public static IEnumerable<PropertySetDef> PropertySetDefs(this System.Type type)
        {
            if(type == null)
            {
                return null;
            }

            IEnumerable<PropertySetDef> propertySetDefs = ActiveSetting.PropertySetDefs;
            if(propertySetDefs == null || propertySetDefs.Count() == 0)
            {
                return null;
            }

            string typeName = type.Name;

            List<PropertySetDef> result = new List<PropertySetDef>();

            propertySetDefs = propertySetDefs.Where(x => x.ApplicableClasses.Any(c => c.ClassName == typeName));
            foreach(PropertySetDef propertySetDef in propertySetDefs)
            {
                if(propertySetDef == null)
                {
                    continue;
                }

                result.Add(propertySetDef);
            }

            return result;
        }
    }
}
