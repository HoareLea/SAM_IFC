using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcLocalPlacement IfcLocalPlacement(this Xbim.Common.IModel model, Spatial.Point3D location, Spatial.Vector3D xAxis = null, Spatial.Vector3D zAxis = null)
        {
            if(location == null || model == null)
            {
                return null;
            }

            IfcLocalPlacement result = model.Instances.New<IfcLocalPlacement>();

            IfcAxis2Placement3D ifcAxis2Placement3D = IfcAxis2Placement3D(model, location);
            if(xAxis != null)
            {
                ifcAxis2Placement3D.RefDirection = xAxis.ToIFC_IfcDirection(model);
            }

            if(zAxis != null)
            {
                ifcAxis2Placement3D.Axis = zAxis.ToIFC_IfcDirection(model);
            }

            result.RelativePlacement = ifcAxis2Placement3D;

            return result;
        }

        public static IfcLocalPlacement IfcLocalPlacement(this Xbim.Common.IModel model, Spatial.Plane plane)
        {
            if (plane == null || model == null)
            {
                return null;
            }

            IfcLocalPlacement result = model.Instances.New<IfcLocalPlacement>();

            IfcAxis2Placement3D ifcAxis2Placement3D = IfcAxis2Placement3D(model, plane.Origin);
            ifcAxis2Placement3D.RefDirection = plane.AxisX.ToIFC_IfcDirection(model);
            ifcAxis2Placement3D.Axis = plane.AxisZ.ToIFC_IfcDirection(model);

            result.RelativePlacement = ifcAxis2Placement3D;

            return result;
        }
    }
}
