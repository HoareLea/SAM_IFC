using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSlabType ToIFC_IfcSlabType(this Construction construction, Xbim.Common.IModel model)
        {
            if(construction == null || model == null)
            {
                return null;
            }

            IfcSlabType result = model.Instances.New<IfcSlabType>();
            result.Name = construction.Name;

            return result;
        }
    }
}
