// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright (c) 2020–2026 Michal Dengusiak & Jakub Ziolkowski and contributors

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

        private static readonly object settingLock = new object();
        private static readonly object definitionsLock = new object();

        private static Setting setting = null;
        private static Definitions<PropertySetDef> definitions;

        private static Setting Load()
        {
            Setting setting = ActiveManager.GetSetting(Assembly.GetExecutingAssembly());
            if (setting == null)
            {
                setting = GetDefault();
            }

            return setting;
        }

        public static Setting Setting
        {
            get
            {
                if (setting == null)
                {
                    lock (settingLock)
                    {
                        if (setting == null)
                        {
                            setting = Load();
                        }
                    }
                }

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
                if (definitions == null)
                {
                    lock (definitionsLock)
                    {
                        if (definitions == null)
                        {
                            definitions = new Definitions<PropertySetDef>(Version.IFC4);
                            definitions.LoadAllDefault();
                        }
                    }
                }

                return definitions?.DefinitionSets;
            }
        }
    }
}