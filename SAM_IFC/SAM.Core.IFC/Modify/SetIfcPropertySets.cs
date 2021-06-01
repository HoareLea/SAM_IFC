using System.Collections.Generic;
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
                return;

            foreach (ParameterSet parameterSet in parameterSets)
            {
                IfcPropertySet ifcPropertySet = parameterSet.ToIFC(databaseIfc);
                IfcRelDefinesByProperties ifcRelDefinesByProperties = new IfcRelDefinesByProperties(ifcObjectDefinition, ifcPropertySet);
            }
        }
    }
}
