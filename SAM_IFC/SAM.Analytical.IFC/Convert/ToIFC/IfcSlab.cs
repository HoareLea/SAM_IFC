using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSlab ToIFC_IfcSlab(this Panel panel, Xbim.Common.IModel model)
        {
            if(panel == null || model == null)
            {
                return null;
            }

            return panel.ToIFC_IfcSlabStandardCase(model);
        }
    }
}
