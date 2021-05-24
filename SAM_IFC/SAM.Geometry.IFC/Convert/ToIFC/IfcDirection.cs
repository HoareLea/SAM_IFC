using Xbim.Ifc;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcDirection ToIFC(this Spatial.Vector3D vector3D, IfcStore ifcStore)
        {
            if(vector3D == null || ifcStore == null)
            {
                return null;
            }

            IfcDirection result = ifcStore.Instances.New<IfcDirection>();
            result.SetXYZ(vector3D.X, vector3D.Y, vector3D.Z);

            return result;
        }
    }
}
