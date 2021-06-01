using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcAxis2Placement3D IfcAxis2Placement3D(this DatabaseIfc databaseIfc, Spatial.Point3D location)
        {
            if(location == null || databaseIfc == null)
            {
                return null;
            }

            IfcAxis2Placement3D result = new IfcAxis2Placement3D(location.ToIFC(databaseIfc));
            return result;
        }

        public static IfcAxis2Placement3D IfcAxis2Placement3D(this DatabaseIfc model, Spatial.Plane plane)
        {
            if (plane == null || model == null)
            {
                return null;
            }

            IfcAxis2Placement3D result = new IfcAxis2Placement3D(plane.Origin.ToIFC(model));
            result.RefDirection = plane.AxisX.ToIFC(model);
            result.Axis = plane.AxisZ.ToIFC(model);

            return result;
        }
    }
}
