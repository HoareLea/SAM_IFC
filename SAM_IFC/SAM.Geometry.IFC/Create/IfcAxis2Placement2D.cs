using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcAxis2Placement2D IfcAxis2Placement2D(this Xbim.Common.IModel model, Planar.Point2D location, Planar.Vector2D xAxis = null)
        {
            if(location == null || model == null)
            {
                return null;
            }

            IfcAxis2Placement2D result = model.Instances.New<IfcAxis2Placement2D>();
            result.Location = location.ToIFC(model);

            if(xAxis != null)
            {
                result.RefDirection = xAxis.ToIFC(model);
            }

            return result;
        }
    }
}
