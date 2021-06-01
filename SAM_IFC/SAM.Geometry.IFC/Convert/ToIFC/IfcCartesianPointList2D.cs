using System;
using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcCartesianPointList2D ToIFC_IfcCartesianPointList2D(this IEnumerable<Planar.Point2D> point2Ds, DatabaseIfc databaseIfc)
        {
            if(point2Ds == null || databaseIfc == null)
            {
                return null;
            }

            List<Tuple<double, double>> tuples = new List<Tuple<double, double>>();
            foreach(Planar.Point2D point2D in point2Ds)
            {
                tuples.Add(new Tuple<double, double>(point2D[0], point2D[1]));
            }

            IfcCartesianPointList2D result = new IfcCartesianPointList2D(databaseIfc, tuples);
            return result;
        }
    }
}
