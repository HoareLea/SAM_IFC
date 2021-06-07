using System.Collections.Generic;
using Xbim.Ifc4.Kernel;

namespace SAM.Core.IFC
{
    public static partial class Modify
    {
        public static void UpdateIfcPropertySet(this IfcProduct ifcProduct, string name)
        {
            if (ifcProduct == null || string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            Xbim.Common.IModel model = ifcProduct.Model;
            if (model == null)
            {
                return;
            }

            //IfcRelDefinesByProperties ifcRelDefinesByProperties = Query.IfcRelDefinesByProperties(ifcProduct, name);
            //if(ifcRelDefinesByProperties == null)
            //{
            //    IfcRelDefinesByProperties ifcRelDefinesByProperties = model.Instances.New<IfcRelDefinesByProperties>();
            //}

            //IfcRelDefinesByProperties relDefinesByProperties
        }
    }
}
