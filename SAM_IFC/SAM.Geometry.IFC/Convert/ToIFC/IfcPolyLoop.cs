using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcPolyLoop ToIFC(this Spatial.IClosedPlanar3D closedPlanar3D, DatabaseIfc databaseIfc)
        {
            if(closedPlanar3D == null || databaseIfc == null)
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

                List<IfcCartesianPoint> ifcCartesianPoints = new List<IfcCartesianPoint>();
                foreach (Spatial.Point3D point3D in point3Ds)
                {
                    IfcCartesianPoint ifcCartesianPoint = point3D?.ToIFC(databaseIfc);
                    if(ifcCartesianPoint == null)
                    {
                        continue;
                    }

                    ifcCartesianPoints.Add(ifcCartesianPoint);
                }

                IfcPolyLoop result = new IfcPolyLoop(ifcCartesianPoints);
                return result;
            }

            return null;
        }
    }
}
