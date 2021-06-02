using System.Collections.Generic;
using System.Linq;
using GeometryGym.Ifc;

namespace SAM.Core.IFC
{
    public static partial class Modify
    {
        public static void SetIfcPropertySets(this IfcObjectDefinition ifcObjectDefinition, SAMObject sAMObject)
        {
            if (ifcObjectDefinition == null || sAMObject == null)
            {
                return;
            }

            DatabaseIfc databaseIfc = ifcObjectDefinition.Database;
            if (databaseIfc == null)
            {
                return;
            }

            List<ParameterSet> parameterSets = sAMObject.GetParamaterSets();
            if (parameterSets == null)
            {
                return;
            }

            foreach (ParameterSet parameterSet in parameterSets)
            {
                IfcPropertySet ifcPropertySet = parameterSet.ToIFC(databaseIfc);
                IfcRelDefinesByProperties ifcRelDefinesByProperties = new IfcRelDefinesByProperties(ifcObjectDefinition, ifcPropertySet);
            }
        }

        public static void SetIfcPropertySets(this IEnumerable<IfcObjectDefinition> ifcObjectDefinitions, SAMObject sAMObject, string sufix)
        {
            if (ifcObjectDefinitions == null || ifcObjectDefinitions.Count() == 0 || sAMObject == null)
            {
                return;
            }

            DatabaseIfc databaseIfc = ifcObjectDefinitions.ElementAt(0)? .Database;
            if (databaseIfc == null)
            {
                return;
            }

            List<ParameterSet> parameterSets = sAMObject.GetParamaterSets();
            if (parameterSets == null)
            {
                return;
            }

            foreach (ParameterSet parameterSet in parameterSets)
            {
                IfcPropertySet ifcPropertySet = parameterSet.ToIFC(databaseIfc, sufix);
                IfcRelDefinesByProperties ifcRelDefinesByProperties = new IfcRelDefinesByProperties(ifcObjectDefinitions, ifcPropertySet);
            }
        }
    }
}
