using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcFacetedBrep ToIFC(this Spatial.Shell shell, DatabaseIfc databaseIfc, double tolerance = Core.Tolerance.Distance)
        {
            if(shell == null || databaseIfc == null)
            {
                return null;
            }

            IfcClosedShell ifcClosedShell = shell.ToIFC_IfcClosedShell(databaseIfc, tolerance);
            if(ifcClosedShell != null)
            {
                IfcFacetedBrep result = new IfcFacetedBrep(ifcClosedShell);
                return result;
            }

            return null;
        }
    }
}
