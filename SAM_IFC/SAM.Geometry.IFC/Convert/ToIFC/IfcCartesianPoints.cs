using System.Collections.Generic;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static List<IfcCartesianPoint> ToIFC(this IEnumerable<Spatial.Point3D> point3Ds, Xbim.Common.IModel model)
        {
            if(point3Ds == null || model == null)
            {
                return null;
            }

            List<IfcCartesianPoint> result = new List<IfcCartesianPoint>();
            foreach (Spatial.Point3D point3D in point3Ds)
            {
                result.Add(point3D?.ToIFC(model));
            }

            return result;
        }

        public static List<IfcCartesianPoint> ToIFC(this IEnumerable<Planar.Point2D> point2Ds, Xbim.Common.IModel model)
        {
            if (point2Ds == null || model == null)
            {
                return null;
            }

            List<IfcCartesianPoint> result = new List<IfcCartesianPoint>();
            foreach (Planar.Point2D point2D in point2Ds)
            {
                result.Add(point2D?.ToIFC(model));
            }

            return result;
        }
    }
}
