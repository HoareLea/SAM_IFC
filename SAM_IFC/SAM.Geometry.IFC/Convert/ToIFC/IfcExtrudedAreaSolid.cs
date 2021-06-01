using SAM.Geometry.Spatial;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcExtrudedAreaSolid ToIFC(this Extrusion extrusion, DatabaseIfc databaseIfc, double tolerance = Core.Tolerance.Distance)
        {
            if(extrusion == null || databaseIfc == null)
            {
                return null;
            }

            Face3D face3D = extrusion.Face3D;
            if(face3D == null)
            {
                return null;
            }

            Plane plane = face3D.GetPlane();
            if(plane == null)
            {
                return null;
            }

            Planar.Face2D face2D = plane.Convert(face3D);
            if (face2D == null)
            {
                return null;
            }

            Vector3D vector3D = extrusion.Vector;
            if (vector3D == null)
            {
                return null;
            }

            Transform3D transform3D = Transform3D.GetOriginToPlane(plane);
            transform3D.Inverse();
            Vector3D direction = Spatial.Query.Transform(vector3D, transform3D)?.GetNormalized();

            IfcProfileDef ifcProfileDef = Create.IfcProfileDef(databaseIfc, face2D, GeometryGym.Ifc.IfcProfileTypeEnum.AREA);
            IfcAxis2Placement3D ifcAxis2Placement3D = Create.IfcAxis2Placement3D(databaseIfc, plane);

            IfcExtrudedAreaSolid result = new IfcExtrudedAreaSolid(ifcProfileDef, ifcAxis2Placement3D, direction.ToIFC(databaseIfc), vector3D.Length);
            return result;
        }
    }
}
