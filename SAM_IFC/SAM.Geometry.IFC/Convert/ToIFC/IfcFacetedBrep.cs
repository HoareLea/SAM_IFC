using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.TopologyResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcFacetedBrep ToIFC(this Spatial.Shell shell, Xbim.Common.IModel model, double tolerance = Core.Tolerance.Distance)
        {
            if(shell == null || model == null)
            {
                return null;
            }

            IfcClosedShell ifcClosedShell = shell.ToIFC_IfcClosedShell(model, tolerance);
            if(ifcClosedShell != null)
            {
                IfcFacetedBrep result = model.Instances.New<IfcFacetedBrep>();
                result.Outer = ifcClosedShell;
                return result;
            }

            return null;
        }
    }
}
