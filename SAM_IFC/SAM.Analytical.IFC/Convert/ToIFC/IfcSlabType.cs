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
            result.PredefinedType = Xbim.Ifc4.Interfaces.IfcSlabTypeEnum.FLOOR;
            result.SetIfcBuildingElementType(construction);
            Core.IFC.Modify.SetIfcPropertySets(result, construction);

            return result;
        }
    }
}
