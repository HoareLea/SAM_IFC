using Xbim.Ifc;
using Xbim.Ifc4.ProfileResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcRectangleProfileDef ToIFC_IfcRectangleProfileDef(this Planar.BoundingBox2D boundingBox2D, Xbim.Common.IModel model)
        {
            if(boundingBox2D == null || model == null)
            {
                return null;
            }

            IfcRectangleProfileDef result = model.Instances.New<IfcRectangleProfileDef>();
            result.Position = boundingBox2D.GetCentroid().ToIFC_IfcAxis2Placement2D(model);

            return result;
        }
    }
}
