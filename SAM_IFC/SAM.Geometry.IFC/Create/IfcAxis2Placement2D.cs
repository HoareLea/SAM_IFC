using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcAxis2Placement2D IfcAxis2Placement2D(this DatabaseIfc databaseIfc, Planar.Point2D location, Planar.Vector2D xAxis = null)
        {
            if(location == null || databaseIfc == null)
            {
                return null;
            }

            IfcAxis2Placement2D result = new IfcAxis2Placement2D(databaseIfc);
            result.Location = location.ToIFC(databaseIfc);

            if(xAxis != null)
            {
                result.RefDirection = xAxis.ToIFC(databaseIfc);
            }

            return result;
        }
    }
}
