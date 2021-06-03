using System.Collections.Generic;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcIndexedPolyCurve ToIFC_IfcIndexedPolyCurve(this Spatial.ISegmentable3D segmentable3D, Xbim.Common.IModel model)
        {
            if(segmentable3D == null || model == null)
            {
                return null;
            }

            List<Spatial.Point3D> point3Ds = segmentable3D.GetPoints();
            if(point3Ds == null || point3Ds.Count < 2)
            {
                return null;
            }

            if (segmentable3D is Spatial.IClosed3D)
            {
                point3Ds.Add(new Spatial.Point3D(point3Ds[0]));
            }

            IfcCartesianPointList3D ifcCartesianPointList3D = point3Ds.ToIFC_IfcCartesianPointList3D(model);
            if(ifcCartesianPointList3D == null)
            {
                return null;
            }

            IfcIndexedPolyCurve result = model.Instances.New<IfcIndexedPolyCurve>();
            result.Points = ifcCartesianPointList3D;


            return result;
        }

        public static IfcIndexedPolyCurve ToIFC_IfcIndexedPolyCurve(this Planar.ISegmentable2D segmentable2D, Xbim.Common.IModel model)
        {
            if (segmentable2D == null || model == null)
            {
                return null;
            }

            List<Planar.Point2D> point2Ds = segmentable2D.GetPoints();
            if (point2Ds == null || point2Ds.Count < 2)
            {
                return null;
            }

            if(segmentable2D is Planar.IClosed2D)
            {
                point2Ds.Add(new Planar.Point2D(point2Ds[0]));
            }

            IfcCartesianPointList2D ifcCartesianPointList2D = point2Ds.ToIFC_IfcCartesianPointList2D(model);
            if (ifcCartesianPointList2D == null)
            {
                return null;
            }

            IfcIndexedPolyCurve result = model.Instances.New<IfcIndexedPolyCurve>();
            result.Points = ifcCartesianPointList2D;

            return result;
        }
    }
}
