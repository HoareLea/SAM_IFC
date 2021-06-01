using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcCartesianPoint ToIFC(this Spatial.Point3D point3D, DatabaseIfc databaseIfc)
        {
            if(point3D == null || databaseIfc == null)
            {
                return null;
            }

            IfcCartesianPoint result = new IfcCartesianPoint(databaseIfc, point3D.X, point3D.Y, point3D.Z);
            return result;
        }

        public static IfcCartesianPoint ToIFC(this Planar.Point2D point2D, DatabaseIfc databaseIfc)
        {
            if (point2D == null || databaseIfc == null)
            {
                return null;
            }

            IfcCartesianPoint result = new IfcCartesianPoint(databaseIfc, point2D.X, point2D.Y);
            return result;
        }
    }
}
