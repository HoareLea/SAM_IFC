using Xbim.Ifc4.Kernel;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcProject ToIFC(this ArchitecturalModel architecturalModel, Xbim.Common.IModel model)
        {
            if(architecturalModel == null || model == null)
            {
                return null;
            }

            IfcProject result = model.Instances.New<IfcProject>();
            result.Initialize(Xbim.Common.ProjectUnits.SIUnitsUK);
            result.UnitsInContext.SetSiLengthUnits(Xbim.Ifc4.Interfaces.IfcSIUnitName.METRE, null);

            result.Name = architecturalModel.Name;

            return result;
        }
    }
}
