using Xbim.Ifc;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcAxis2Placement2D ToIFC_IfcAxis2Placement2D(this Planar.Point2D point2D, IfcStore ifcStore)
        {
            if(point2D == null || ifcStore == null)
            {
                return null;
            }

            IfcAxis2Placement2D result = ifcStore.Instances.New<IfcAxis2Placement2D>();
            result.Location = point2D.ToIFC(ifcStore);

            return result;
        }
    }
}
