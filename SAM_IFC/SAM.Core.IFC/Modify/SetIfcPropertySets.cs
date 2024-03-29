﻿using System.Collections.Generic;
using Xbim.Ifc4.Kernel;

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

            Xbim.Common.IModel model = ifcObjectDefinition.Model;
            if (model == null)
            {
                return;
            }

            List<ParameterSet> parameterSets = sAMObject.GetParameterSets();
            if (parameterSets == null)
                return;

            foreach (ParameterSet parameterSet in parameterSets)
            {
                IfcPropertySet ifcPropertySet = parameterSet.ToIFC(model);

                if (ifcObjectDefinition is IfcTypeObject)
                {
                    IfcTypeObject ifcTypeObject = (IfcTypeObject)ifcObjectDefinition;
                    ifcTypeObject.HasPropertySets.Add(ifcPropertySet);
                }
                else
                {
                    IfcRelDefinesByProperties ifcRelDefinesByProperties = model.Instances.New<IfcRelDefinesByProperties>();
                    ifcRelDefinesByProperties.RelatedObjects.Add(ifcObjectDefinition);
                    ifcRelDefinesByProperties.RelatingPropertyDefinition = ifcPropertySet;
                }

            }
        }
    }
}
