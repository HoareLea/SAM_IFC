using System.Collections.Generic;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.TopologyResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcClosedShell ToIFC_IfcClosedShell(this Spatial.Shell shell, Xbim.Common.IModel model, double tolerance = Core.Tolerance.Distance)
        {
            if(shell == null || model == null)
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

                IfcClosedShell result = model.Instances.New<IfcClosedShell>();
                foreach (Spatial.Face3D face3D in face3Ds)
                {
                    IfcFace ifcFace = face3D?.ToIFC(model);
                    if(ifcFace ==null)
                    {
                        continue;
                    }

                    result.CfsFaces.Add(ifcFace);
                }

                return result;
            }

            return null;
        }
    }
}
