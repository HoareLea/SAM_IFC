using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcWallType ToIFC_IfcWallType(this Construction construction, Xbim.Common.IModel model)
        {
            if(construction == null || model == null)
            {
                return null;
            }

            IfcWallType result = model.Instances.New<IfcWallType>();
            result.Name = construction.Name;
            Core.IFC.Modify.SetIfcPropertySets(result, construction);

            return result;
        }
    }
}
