using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcVector ToIFC(this Spatial.Vector3D vector3D, Xbim.Common.IModel model)
        {
            if(vector3D == null || model == null)
            {
                return null;
            }

            IfcVector result = model.Instances.New<IfcVector>();
            result.Orientation = model.Instances.New<IfcDirection>();
            result.Orientation.SetXYZ(vector3D.X, vector3D.Y, vector3D.Z);
            result.Magnitude = vector3D.Length;

            return result;
        }

        public static IfcVector ToIFC(this Planar.Vector2D vector2D, Xbim.Common.IModel model)
        {
            IfcVector result = model.Instances.New<IfcVector>();
            result.Orientation = model.Instances.New<IfcDirection>();
            result.Orientation.SetXY(vector2D.X, vector2D.Y);
            result.Magnitude = vector2D.Length;

            return result;
        }
    }
}
