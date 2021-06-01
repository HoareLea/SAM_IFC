using System.Collections.Generic;
using SAM.Geometry.Spatial;

namespace SAM.Geometry.IFC
{
    public static partial class Query
    {
        public static Planar.Segment2D Axis(this Face3D face3D)
        {
            if(face3D == null)
            {
                return null;
            }

            ISegmentable3D segmentable3D = face3D.GetExternalEdge3D() as ISegmentable3D;
            if(segmentable3D == null)
            {
                throw new System.NotImplementedException();
            }


            Plane plane = face3D.GetPlane();
            if(plane == null)
            {
                return null;
            }

            Vector3D vector3D = System.Math.Abs(plane.AxisY.Z) < System.Math.Abs(plane.AxisX.Z) ? plane.AxisY : plane.AxisX;

            List<Point3D> point3Ds = segmentable3D.GetPoints()?.ConvertAll(x => vector3D.Project(x));
            if(point3Ds == null || point3Ds.Count < 2)
            {
                return null;
            }

            point3Ds.ExtremePoints(out Point3D point3D_1, out Point3D point3D_2);
            if(point3D_1 == point3D_2 || point3D_1 == null || point3D_2 == null)
            {
                return null;
            }

            return new Planar.Segment2D(plane.Convert(point3D_1), plane.Convert(point3D_2));
        }
    }
}