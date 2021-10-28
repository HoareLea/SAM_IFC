using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcRoof ToIFC_IfcRoof(this Roof roof, Xbim.Common.IModel model)
        {
            if(roof == null || model == null)
            {
                return null;
            }

            IfcRoof result = model.Instances.New<IfcRoof>();
            result.SetIfcBuildingElement(roof);
            result.SetIfcProductRepresentation(roof);
            Core.IFC.Modify.SetIfcPropertySets(result, roof);

            return result;
        }
    }
}
