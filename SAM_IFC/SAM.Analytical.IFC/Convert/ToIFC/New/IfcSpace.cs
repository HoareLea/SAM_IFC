using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSpace ToIFC(this Space space, Xbim.Common.IModel model, ArchitecturalModel architecturalModel)
        {
            if (space == null || model == null)
            {
                return null;
            }

            IfcSpace result = model.Instances.New<IfcSpace>();
            result.Name = space.Name;
            result.LongName = space.Name;
            //result.ObjectType = typeof(Space).Name;
            result.PredefinedType = Xbim.Ifc4.Interfaces.IfcSpaceTypeEnum.SPACE;
            Modify.SetIfcProductRepresentation(result, space, architecturalModel);
            Core.IFC.Modify.SetIfcPropertySets(result, space);

            return result;
        }
    }
}
