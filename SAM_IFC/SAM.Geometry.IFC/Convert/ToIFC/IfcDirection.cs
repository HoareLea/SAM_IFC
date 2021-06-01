using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcDirection ToIFC(this Spatial.Vector3D vector3D, DatabaseIfc databaseIfc)
        {
            if(vector3D == null || databaseIfc == null)
            {
                return null;
            }

            IfcDirection result = new IfcDirection(databaseIfc, vector3D[0], vector3D[1], vector3D[2]);
            return result;
        }

        public static IfcDirection ToIFC(this Planar.Vector2D vector2D, DatabaseIfc databaseIfc)
        {
            if (vector2D == null || databaseIfc == null)
            {
                return null;
            }

            IfcDirection result = new IfcDirection(databaseIfc, vector2D[0], vector2D[1]);
            return result;
        }
    }
}
