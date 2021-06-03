using System.Collections.Generic;
using Xbim.Ifc4.TopologyResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcFace ToIFC(this Spatial.Face3D face3D, Xbim.Common.IModel model)
        {
            if(face3D == null || model == null)
            {
                return null;
            }

            IfcPolyLoop ifcPolyLoop = face3D.GetExternalEdge3D()?.ToIFC(model);
            if(ifcPolyLoop == null)
            {
                return null;
            }

            IfcFaceOuterBound ifcFaceOuterBound = model.Instances.New<IfcFaceOuterBound>();
            ifcFaceOuterBound.Bound = ifcPolyLoop;

            IfcFace result = model.Instances.New<IfcFace>();
            result.Bounds.Add(ifcFaceOuterBound);
             
            List<Spatial.IClosedPlanar3D> internalEdge3Ds = face3D.GetInternalEdge3Ds();
            if(internalEdge3Ds != null && internalEdge3Ds.Count != 0)
            {
                foreach(Spatial.IClosedPlanar3D internalEdge3D in internalEdge3Ds)
                {
                    ifcPolyLoop = internalEdge3D?.ToIFC(model);
                    if(ifcPolyLoop == null)
                    {
                        continue;
                    }

                    IfcFaceBound ifcFaceBound = model.Instances.New<IfcFaceBound>();
                    ifcFaceBound.Bound = ifcPolyLoop;
                    result.Bounds.Add(ifcFaceBound);
                }
            }

            return result;
        }
    }
}
