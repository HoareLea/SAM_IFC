using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcRoofType ToIFC_IfcRoofType(this RoofType roofType, Xbim.Common.IModel model)
        {
            if(roofType == null || model == null)
            {
                return null;
            }

            IfcRoofType result = model.Instances.New<IfcRoofType>();
            result.SetIfcBuildingElementType(roofType);
            Core.IFC.Modify.SetIfcPropertySets(result, roofType);

            return result;
        }
    }
}
