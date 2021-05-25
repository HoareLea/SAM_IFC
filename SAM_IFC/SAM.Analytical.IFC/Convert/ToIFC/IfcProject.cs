using Xbim.Ifc4.Kernel;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcProject ToIFC(this AnalyticalModel analyticalModel, Xbim.Common.IModel model)
        {
            if(analyticalModel == null || model == null)
            {
                return null;
            }

            IfcProject result = model.Instances.New<IfcProject>();
            result.Initialize(Xbim.Common.ProjectUnits.SIUnitsUK);
            result.Name = analyticalModel.Name;

            return result;
        }
    }
}
