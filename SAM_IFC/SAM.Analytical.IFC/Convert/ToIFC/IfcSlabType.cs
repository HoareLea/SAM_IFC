using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSlabType ToIFC_IfcSlabType(this Construction construction, Xbim.Common.IModel model, PanelGroup panelGroup = PanelGroup.Floor)
        {
            if(construction == null || model == null)
            {
                return null;
            }

            IfcSlabType result = model.Instances.New<IfcSlabType>();
            result.PredefinedType = Query.IfcSlabTypeEnum(panelGroup);
            result.SetIfcBuildingElementType(construction);
            Core.IFC.Modify.SetIfcPropertySets(result, construction);

            return result;
        }
    }
}
