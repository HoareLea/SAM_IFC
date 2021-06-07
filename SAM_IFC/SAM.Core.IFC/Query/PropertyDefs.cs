using System.Collections.Generic;
using System.Linq;
using Xbim.Properties;

namespace SAM.Core.IFC
{
    public static partial class Query
    {
        public static IEnumerable<PropertyDef> PropertyDefs(this System.Type type, string name)
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

            List<PropertyDef> result = new List<PropertyDef>();

            propertySetDefs = propertySetDefs.Where(x => x.ApplicableClasses.Any(c => c.ClassName == typeName));
            foreach(PropertySetDef propertySetDef in propertySetDefs)
            {
                List<PropertyDef> propertyDefs = propertySetDef.PropertyDefinitions;
                if(propertySetDefs == null || propertyDefs.Count == 0)
                {
                    continue;
                }

                foreach (PropertyDef propertyDef in propertyDefs)
                {
                    if(propertyDef == null)
                    {
                        continue;
                    }

                    if(!string.IsNullOrWhiteSpace(name))
                    {
                        if(!name.Equals(propertyDef.Name))
                        {
                            continue;
                        }
                    }
                    
                    result.Add(propertyDef);
                }
            }

            return result;
        }
    }
}
