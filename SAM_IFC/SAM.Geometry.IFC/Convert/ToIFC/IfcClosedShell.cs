using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcClosedShell ToIFC_IfcClosedShell(this Spatial.Shell shell, DatabaseIfc databaseIfc, double tolerance = Core.Tolerance.Distance)
        {
            if(shell == null || databaseIfc == null)
            {
                return null;
            }

            if(shell.IsClosed(tolerance))
            {
                List<Spatial.Face3D> face3Ds = shell.Face3Ds;
                if(face3Ds == null || face3Ds.Count < 3)
                {
                    return null;
                }

                List<IfcFace> faces = new List<IfcFace>();
                foreach (Spatial.Face3D face3D in face3Ds)
                {
                    IfcFace ifcFace = face3D?.ToIFC(databaseIfc);
                    if(ifcFace ==null)
                    {
                        continue;
                    }

                    faces.Add(ifcFace);
                }

                IfcClosedShell result = new IfcClosedShell(faces);
                return result;
            }

            return null;
        }
    }
}
