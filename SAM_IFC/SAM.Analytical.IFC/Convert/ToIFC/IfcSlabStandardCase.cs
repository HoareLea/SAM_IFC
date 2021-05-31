using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSlabStandardCase ToIFC_IfcSlabStandardCase(this Panel panel, Xbim.Common.IModel model)
        {
            if(panel == null || model == null)
            {
                return null;
            }

            IfcSlabStandardCase result = model.Instances.New<IfcSlabStandardCase>();
            result.PredefinedType = Xbim.Ifc4.Interfaces.IfcSlabTypeEnum.FLOOR;
            result.SetIfcBuildingElement(panel);
            result.SetIfcProductRepresentation(panel);
            Core.IFC.Modify.SetIfcPropertySets(result, panel);

            return result;
        }
    }
}
