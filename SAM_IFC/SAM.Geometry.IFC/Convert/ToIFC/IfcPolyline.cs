using System.Collections.Generic;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcPolyline ToIFC_IfcPolyline(this Spatial.Polygon3D polygon3D, Xbim.Common.IModel model)
        {
            if(polygon3D == null || model == null)
            {
                return null;
            }

            IfcPolyline result = model.Instances.New<IfcPolyline>();

            List<Spatial.Point3D> point3Ds = polygon3D.GetPoints();
            if(point3Ds != null && point3Ds.Count == 0)
            {
                result.Points.AddRange(point3Ds.ToIFC(model));
            }
            
            return result;
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.Polygon2D polygon2D, Xbim.Common.IModel model)
        {
            return ToIFC_IfcPolyline((Planar.ISegmentable2D)polygon2D, model);
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.Rectangle2D rectangle2D, Xbim.Common.IModel model)
        {
            return ToIFC_IfcPolyline((Planar.ISegmentable2D)rectangle2D, model);
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.Triangle2D triangle2D, Xbim.Common.IModel model)
        {
            return ToIFC_IfcPolyline((Planar.ISegmentable2D)triangle2D, model);
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.Polyline2D polyline2D, Xbim.Common.IModel model)
        {
            return ToIFC_IfcPolyline((Planar.ISegmentable2D)polyline2D, model);
        }

        public static IfcPolyline ToIFC_IfcPolyline(this Planar.ISegmentable2D segmentable2D, Xbim.Common.IModel model)
        {
            if (segmentable2D == null || model == null)
            {
                return null;
            }

            IfcPolyline result = model.Instances.New<IfcPolyline>();

            List<Planar.Point2D> point2Ds = segmentable2D.GetPoints();
            if (point2Ds != null && point2Ds.Count != 0)
            {
                result.Points.AddRange(point2Ds.ToIFC(model));
            }

            return result;
        }
    }
}
