using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static List<IfcCartesianPoint> ToIFC(this IEnumerable<Spatial.Point3D> point3Ds, DatabaseIfc databaseIfc)
        {
            if(point3Ds == null || databaseIfc == null)
            {
                return null;
            }

            List<IfcCartesianPoint> result = new List<IfcCartesianPoint>();
            foreach (Spatial.Point3D point3D in point3Ds)
            {
                result.Add(point3D?.ToIFC(databaseIfc));
            }

            return result;
        }

        public static List<IfcCartesianPoint> ToIFC(this IEnumerable<Planar.Point2D> point2Ds, DatabaseIfc databaseIfc)
        {
            if (point2Ds == null || databaseIfc == null)
            {
                return null;
            }

            List<IfcCartesianPoint> result = new List<IfcCartesianPoint>();
            foreach (Planar.Point2D point2D in point2Ds)
            {
                result.Add(point2D?.ToIFC(databaseIfc));
            }

            return result;
        }
    }
}
