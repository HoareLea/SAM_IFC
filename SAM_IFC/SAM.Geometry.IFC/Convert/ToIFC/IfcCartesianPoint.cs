using Xbim.Ifc;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcCartesianPoint ToIFC(this Spatial.Point3D point3D, Xbim.Common.IModel model)
        {
            if(point3D == null || model == null)
            {
                return null;
            }

            IfcCartesianPoint result = model.Instances.New<IfcCartesianPoint>();
            result.SetXYZ(point3D.X, point3D.Y, point3D.Z);

            return result;
        }

        public static IfcCartesianPoint ToIFC(this Planar.Point2D point2D, Xbim.Common.IModel model)
        {
            if (point2D == null || model == null)
            {
                return null;
            }

            IfcCartesianPoint result = model.Instances.New<IfcCartesianPoint>();
            result.SetXY(point2D.X, point2D.Y);

            return result;
        }
    }
}
