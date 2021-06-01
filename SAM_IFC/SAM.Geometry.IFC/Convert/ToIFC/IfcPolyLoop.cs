using System.Collections.Generic;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.TopologyResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcPolyLoop ToIFC(this Spatial.IClosedPlanar3D closedPlanar3D, Xbim.Common.IModel model)
        {
            if(closedPlanar3D == null || model == null)
            {
                return null;
            }

            Spatial.IClosedPlanar3D closedPlanar3D_Temp = closedPlanar3D;
            if(closedPlanar3D is Spatial.Face3D)
            {
                closedPlanar3D_Temp = ((Spatial.Face3D)closedPlanar3D).GetExternalEdge3D();
            }

            if (closedPlanar3D_Temp is Spatial.ISegmentable3D)
            {
                List<Spatial.Point3D> point3Ds = ((Spatial.ISegmentable3D)closedPlanar3D_Temp).GetPoints();
                if(point3Ds == null || point3Ds.Count == 0)
                {
                    return null;
                }

                IfcPolyLoop result = model.Instances.New<IfcPolyLoop>();
                foreach(Spatial.Point3D point3D in point3Ds)
                {
                    IfcCartesianPoint ifcCartesianPoint = point3D?.ToIFC(model);
                    if(ifcCartesianPoint == null)
                    {
                        continue;
                    }

                    result.Polygon.Add(ifcCartesianPoint);
                }

                return result;
            }

            return null;
        }
    }
}
