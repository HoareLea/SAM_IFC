using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcPolyline ToIFC_IfcPolyline(this Spatial.Polygon3D polygon3D, DatabaseIfc databaseIfc)
        {
            if(polygon3D == null || databaseIfc == null)
            {
                return null;
            }

            List<Spatial.Point3D> point3Ds = polygon3D.GetPoints();
            if(point3Ds != null && point3Ds.Count == 0)
            {
                IfcPolyline result = new IfcPolyline(point3Ds.ToIFC(databaseIfc));
                return result;
            }
            
            return null;
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.Polygon2D polygon2D, DatabaseIfc databaseIfc)
        {
            return ToIFC_IfcPolyline((Planar.ISegmentable2D)polygon2D, databaseIfc);
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.Rectangle2D rectangle2D, DatabaseIfc databaseIfc)
        {
            return ToIFC_IfcPolyline((Planar.ISegmentable2D)rectangle2D, databaseIfc);
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.Triangle2D triangle2D, DatabaseIfc databaseIfc)
        {
            return ToIFC_IfcPolyline((Planar.ISegmentable2D)triangle2D, databaseIfc);
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.Polyline2D polyline2D, DatabaseIfc databaseIfc)
        {
            return ToIFC_IfcPolyline((Planar.ISegmentable2D)polyline2D, databaseIfc);
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.ISegmentable2D segmentable2D, DatabaseIfc databaseIfc)
        {
            if (segmentable2D == null || databaseIfc == null)
            {
                return null;
            }

            List<Planar.Point2D> point2Ds = segmentable2D.GetPoints();
            if (point2Ds != null && point2Ds.Count != 0)
            {
                IfcPolyline result = new IfcPolyline(point2Ds.ToIFC(databaseIfc));
                return result;
            }

            return null;
        }
    }
}
