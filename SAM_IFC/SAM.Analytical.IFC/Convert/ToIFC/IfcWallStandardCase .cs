using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcWallStandardCase ToIFC(this Panel panel, Xbim.Common.IModel model)
        {
            if(panel == null || model == null)
            {
                return null;
            }

            IfcWallStandardCase result = model.Instances.New<IfcWallStandardCase>();

            return result;
        }
    }
}
