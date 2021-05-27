using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcRoofType ToIFC_IfcRoofType(this Construction construction, Xbim.Common.IModel model)
        {
            if(construction == null || model == null)
            {
                return null;
            }

            IfcRoofType result = model.Instances.New<IfcRoofType>();
            result.Name = construction.Name;

            return result;
        }
    }
}
