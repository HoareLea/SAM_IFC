using Xbim.Ifc;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcAxis2Placement3D ToIFC_IfcAxis2Placement3D(this Spatial.Point3D point3D, IfcStore ifcStore)
        {
            if(point3D == null || ifcStore == null)
            {
                return null;
            }

            IfcAxis2Placement3D result = ifcStore.Instances.New<IfcAxis2Placement3D>();
            result.Location = point3D.ToIFC(ifcStore);

            return result;
        }
    }
}
