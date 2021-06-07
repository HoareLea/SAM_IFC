using System.Collections.Generic;
using System.Reflection;
using Xbim.Properties;

namespace SAM.Core.IFC
{
    public static partial class ActiveSetting
    {
        public static class Name
        {

        }

        private static Setting setting = Load();

        private static Definitions<PropertySetDef> definitions;

        private static Setting Load()
        {
            Setting setting = ActiveManager.GetSetting(Assembly.GetExecutingAssembly());
            if (setting == null)
                setting = GetDefault();

            return setting;
        }

        public static Setting Setting
        {
            get
            {
                return setting;
            }
        }

        public static Setting GetDefault()
        {
            Setting setting = new Setting(Assembly.GetExecutingAssembly());

            return setting;
        }

        public static IEnumerable<PropertySetDef> PropertySetDefs
        {
            get
            {
                if(definitions == null)
                {
                    definitions = new Definitions<PropertySetDef>(Version.IFC4);
                    definitions.LoadAllDefault();
                }

                return definitions?.DefinitionSets;
            }
        }
    }
}