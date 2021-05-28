using Xbim.Ifc4.MaterialResource;

namespace SAM.Core.IFC
{
    public static partial class Convert
    {
        public static IfcMaterial ToIFC(this IMaterial material, Xbim.Common.IModel model)
        {
            if(material == null || model == null)
            {
                return null;
            }

            IfcMaterial result = model.Instances.New<IfcMaterial>();
            result.Name = material.Name;

            return result;
        }
    }
}
