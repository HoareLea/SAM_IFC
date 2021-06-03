using Xbim.Common.Geometry;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static Spatial.Point3D ToSAM_Point3D(this IXbimPoint xbimPoint)
        {
            if(xbimPoint == null)
            {
                return null;
            }

            return new Spatial.Point3D(xbimPoint.X, xbimPoint.Y, xbimPoint.Z);

        }
        
        public static Planar.Point2D ToSAM_Point2D(this IXbimPoint xbimPoint)
        {
            if (xbimPoint == null)
            {
                return null;
            }

            return new Planar.Point2D(xbimPoint.X, xbimPoint.Y);
        }
    }
}
