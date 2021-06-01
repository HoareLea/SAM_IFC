using System;
using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcCartesianPointList3D ToIFC_IfcCartesianPointList3D(this IEnumerable<Spatial.Point3D> point3Ds, DatabaseIfc databaseIfc)
        {
            if (point3Ds == null || databaseIfc == null)
            {
                return null;
            }

            List<Tuple<double, double, double>> tuples = new List<Tuple<double, double, double>>();
            foreach (Spatial.Point3D point3D in point3Ds)
            {
                tuples.Add(new Tuple<double, double, double>(point3D[0], point3D[1], point3D[2]));
            }

            IfcCartesianPointList3D result = new IfcCartesianPointList3D(databaseIfc, tuples);
            return result;
        }
    }
}
