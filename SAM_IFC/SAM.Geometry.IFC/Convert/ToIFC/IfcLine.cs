using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcLine ToIFC(this Spatial.Segment3D segment3D, Xbim.Common.IModel model)
        {
            if(segment3D == null || model == null)
            {
                return null;
            }

            IfcLine result = model.Instances.New<IfcLine>();
            result.Pnt = segment3D[0].ToIFC(model);
            result.Dir = (segment3D.Direction * segment3D.GetLength()).ToIFC(model);

            return result;
        }

        public static IfcLine ToIFC(this Planar.Segment2D segment2D, Xbim.Common.IModel model)
        {
            if (segment2D == null || model == null)
            {
                return null;
            }

            IfcLine result = model.Instances.New<IfcLine>();
            result.Pnt = segment2D[0].ToIFC(model);
            result.Dir = (segment2D.Direction * segment2D.GetLength()).ToIFC(model);

            return result;
        }
    }
}
