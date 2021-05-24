using Xbim.Ifc;
using Xbim.Ifc4.ProfileResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcRectangleProfileDef ToIFC_IfcRectangleProfileDef(this Planar.Rectangle2D rectangle2D, IfcStore ifcStore)
        {
            if(rectangle2D == null || ifcStore == null)
            {
                return null;
            }

            IfcRectangleProfileDef result = ifcStore.Instances.New<IfcRectangleProfileDef>();
            result.Position = rectangle2D.GetCentroid().ToIFC_IfcAxis2Placement2D(ifcStore);

            return result;
        }
    }
}
