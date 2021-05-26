using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProfileResource;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcRectangleProfileDef IfcRectangleProfileDef(this Xbim.Common.IModel model, Planar.Rectangle2D rectangle2D, IfcProfileTypeEnum ifcProfileTypeEnum)
        {
            if(rectangle2D == null || model == null)
            {
                return null;
            }

            IfcRectangleProfileDef result = model.Instances.New<IfcRectangleProfileDef>();
            result.Position = IfcAxis2Placement2D(model, rectangle2D.GetCentroid(), rectangle2D.WidthDirection);
            result.ProfileType = ifcProfileTypeEnum;
            result.XDim = rectangle2D.Width;
            result.YDim = rectangle2D.Height;

            return result;
        }

        public static IfcRectangleProfileDef IfcRectangleProfileDef(this Xbim.Common.IModel model, Planar.BoundingBox2D boundingBox2D, IfcProfileTypeEnum ifcProfileTypeEnum)
        {
            IfcRectangleProfileDef result = model.Instances.New<IfcRectangleProfileDef>();
            result.Position = IfcAxis2Placement2D(model, boundingBox2D.GetCentroid());
            result.ProfileType = ifcProfileTypeEnum;
            result.XDim = boundingBox2D.Width;
            result.YDim = boundingBox2D.Height;

            return result;
        }
    }
}
