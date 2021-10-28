using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSlab ToIFC_IfcSlab(this Floor floor, Xbim.Common.IModel model)
        {
            if(floor == null || model == null)
            {
                return null;
            }

            IfcSlab result = model.Instances.New<IfcSlab>();
            result.PredefinedType = Xbim.Ifc4.Interfaces.IfcSlabTypeEnum.FLOOR;
            result.SetIfcBuildingElement(floor);
            result.SetIfcProductRepresentation(floor);
            Core.IFC.Modify.SetIfcPropertySets(result, floor);

            return result;
        }

        public static IfcSlab ToIFC_IfcSlab(this Roof roof, Xbim.Common.IModel model)
        {
            if (roof == null || model == null)
            {
                return null;
            }

            IfcSlab result = model.Instances.New<IfcSlab>();
            result.PredefinedType = Xbim.Ifc4.Interfaces.IfcSlabTypeEnum.ROOF;
            result.SetIfcBuildingElement(roof);
            result.SetIfcProductRepresentation(roof);
            Core.IFC.Modify.SetIfcPropertySets(result, roof);

            return result;
        }
    }
}
