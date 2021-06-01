using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcRectangleProfileDef IfcRectangleProfileDef(this DatabaseIfc databaseIfc, Planar.Rectangle2D rectangle2D, IfcProfileTypeEnum ifcProfileTypeEnum)
        {
            if(rectangle2D == null || databaseIfc == null)
            {
                return null;
            }

            IfcRectangleProfileDef result = new IfcRectangleProfileDef(databaseIfc, string.Empty, rectangle2D.Width, rectangle2D.Height);
            result.Position = IfcAxis2Placement2D(databaseIfc, rectangle2D.GetCentroid(), rectangle2D.WidthDirection);
            result.ProfileType = ifcProfileTypeEnum;
            return result;
        }

        public static IfcRectangleProfileDef IfcRectangleProfileDef(this DatabaseIfc databaseIfc, Planar.BoundingBox2D boundingBox2D, IfcProfileTypeEnum ifcProfileTypeEnum)
        {
            IfcRectangleProfileDef result = new IfcRectangleProfileDef(databaseIfc, string.Empty, boundingBox2D.Width, boundingBox2D.Height);
            result.Position = IfcAxis2Placement2D(databaseIfc, boundingBox2D.GetCentroid());
            result.ProfileType = ifcProfileTypeEnum;
            return result;
        }
    }
}
