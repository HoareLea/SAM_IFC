using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcLocalPlacement IfcLocalPlacement(this DatabaseIfc databaseIfc, Spatial.Point3D location, Spatial.Vector3D xAxis = null, Spatial.Vector3D zAxis = null)
        {
            if(location == null || databaseIfc == null)
            {
                return null;
            }

            IfcAxis2Placement3D ifcAxis2Placement3D = IfcAxis2Placement3D(databaseIfc, location);
            if(xAxis != null)
            {
                ifcAxis2Placement3D.RefDirection = xAxis.ToIFC(databaseIfc);
            }

            if(zAxis != null)
            {
                ifcAxis2Placement3D.Axis = zAxis.ToIFC(databaseIfc);
            }

            IfcLocalPlacement result = new IfcLocalPlacement(ifcAxis2Placement3D);
            return result;
        }

        public static IfcLocalPlacement IfcLocalPlacement(this DatabaseIfc databaseIfc, Spatial.Plane plane)
        {
            if (plane == null || databaseIfc == null)
            {
                return null;
            }

            IfcAxis2Placement3D ifcAxis2Placement3D = IfcAxis2Placement3D(databaseIfc, plane.Origin);
            ifcAxis2Placement3D.RefDirection = plane.AxisX.ToIFC(databaseIfc);
            ifcAxis2Placement3D.Axis = plane.AxisZ.ToIFC(databaseIfc);

            IfcLocalPlacement result = new IfcLocalPlacement(ifcAxis2Placement3D);

            return result;
        }
    }
}
