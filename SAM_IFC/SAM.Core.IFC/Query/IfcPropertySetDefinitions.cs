using System.Collections.Generic;
using System.Linq;
using Xbim.Ifc4.Kernel;

namespace SAM.Core.IFC
{
    public static partial class Query
    {
        public static List<Xbim.Ifc4.Interfaces.IIfcPropertySetDefinition> IfcPropertySetDefinitions(this IfcProduct ifcProduct)
        {
            IEnumerable<IfcRelDefinesByProperties> ifcRelDefinesByPropertiesList = ifcProduct?.IsDefinedBy;
            if (ifcRelDefinesByPropertiesList == null && ifcRelDefinesByPropertiesList == null)
            {
                return null;
            }


            List<Xbim.Ifc4.Interfaces.IIfcPropertySetDefinition> result = new List<Xbim.Ifc4.Interfaces.IIfcPropertySetDefinition>();
            foreach (IfcRelDefinesByProperties ifcRelDefinesByProperties in ifcRelDefinesByPropertiesList)
            {
                IfcPropertySetDefinitionSelect ifcPropertySetDefinitionSelect = ifcRelDefinesByProperties.RelatingPropertyDefinition;

                IEnumerable<Xbim.Ifc4.Interfaces.IIfcPropertySetDefinition> ifcPropertySetDefinitions = ifcPropertySetDefinitionSelect?.PropertySetDefinitions;
                if(ifcPropertySetDefinitions == null || ifcPropertySetDefinitions.Count() == 0)
                {
                    continue;
                }

                foreach (Xbim.Ifc4.Interfaces.IIfcPropertySetDefinition ifcPropertySetDefinition in ifcPropertySetDefinitions)
                {
                    result.Add(ifcPropertySetDefinition);
                }

            }

            return result;
        }
    }
}
