using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcAxis2Placement3D ToIFC_IfcAxis2Placement3D(this Spatial.Point3D point3D, Xbim.Common.IModel model)
        {
            if(point3D == null || model == null)
            {
                return null;
            }

            IfcAxis2Placement3D result = model.Instances.New<IfcAxis2Placement3D>();
            result.Location = point3D.ToIFC(model);

            return result;
        }
    }
}
