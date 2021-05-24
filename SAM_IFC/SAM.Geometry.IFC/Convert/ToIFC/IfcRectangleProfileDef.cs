using Xbim.Ifc;
using Xbim.Ifc4.ProfileResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcRectangleProfileDef ToIFC_IfcRectangleProfileDef(this Planar.BoundingBox2D boundingBox2D, IfcStore ifcStore)
        {
            if(boundingBox2D == null || ifcStore == null)
            {
                return null;
            }

            IfcRectangleProfileDef result = ifcStore.Instances.New<IfcRectangleProfileDef>();

            return result;
        }
    }
}
