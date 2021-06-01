using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcFace ToIFC(this Spatial.Face3D face3D, DatabaseIfc databaseIfc, bool orientation = false)
        {
            if(face3D == null || databaseIfc == null)
            {
                return null;
            }

            IfcPolyLoop ifcPolyLoop = face3D.GetExternalEdge3D()?.ToIFC(databaseIfc);
            if(ifcPolyLoop == null)
            {
                return null;
            }

            IfcFaceOuterBound ifcFaceOuterBound = new IfcFaceOuterBound(ifcPolyLoop, orientation);

            IfcFace result = new IfcFace(ifcFaceOuterBound);
             
            List<Spatial.IClosedPlanar3D> internalEdge3Ds = face3D.GetInternalEdge3Ds();
            if(internalEdge3Ds != null && internalEdge3Ds.Count != 0)
            {
                foreach(Spatial.IClosedPlanar3D internalEdge3D in internalEdge3Ds)
                {
                    ifcPolyLoop = internalEdge3D?.ToIFC(databaseIfc);
                    if(ifcPolyLoop == null)
                    {
                        continue;
                    }

                    IfcFaceBound ifcFaceBound = new IfcFaceBound(ifcPolyLoop, orientation);
                    ifcFaceBound.Bound = ifcPolyLoop;
                    result.Bounds.Add(ifcFaceBound);
                }
            }

            return result;
        }
    }
}
