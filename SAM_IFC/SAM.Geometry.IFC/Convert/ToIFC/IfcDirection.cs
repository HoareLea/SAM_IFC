using Xbim.Ifc;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcDirection ToIFC(this Spatial.Vector3D vector3D, Xbim.Common.IModel model)
        {
            if(vector3D == null || model == null)
            {
                return null;
            }

            IfcDirection result = model.Instances.New<IfcDirection>();
            result.SetXYZ(vector3D.X, vector3D.Y, vector3D.Z);

            return result;
        }
    }
}
