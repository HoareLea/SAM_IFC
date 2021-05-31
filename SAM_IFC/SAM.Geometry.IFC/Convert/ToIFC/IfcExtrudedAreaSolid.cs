using SAM.Geometry.Spatial;
using Xbim.Ifc4.GeometricModelResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcExtrudedAreaSolid ToIFC(this Extrusion extrusion, Xbim.Common.IModel model, double tolerance = Core.Tolerance.Distance)
        {
            if(extrusion == null || model == null)
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

            IfcExtrudedAreaSolid result = model.Instances.New<IfcExtrudedAreaSolid>();
            result.Position = Create.IfcAxis2Placement3D(model, plane);
            result.Depth = vector3D.Length;
            result.ExtrudedDirection = direction.ToIFC(model);// extrusion.Vector.GetNormalized().ToIFC(model);
            result.SweptArea = Create.IfcProfileDef(model, face2D, Xbim.Ifc4.Interfaces.IfcProfileTypeEnum.AREA);
            return result;

            //Vector3D normal = plane.Normal;

            //if (normal.AlmostEqual(Vector3D.WorldZ, tolerance) || normal.AlmostEqual(Vector3D.WorldZ.GetNegated(), tolerance))
            //{

            //}

            //if (Plane.WorldXY.Perpendicular(plane, tolerance))
            //{
            //    Planar.IClosed2D externalEdge2D = face2D.ExternalEdge2D;
            //    if (externalEdge2D == null)
            //    {
            //        return null;
            //    }

            //    List<Planar.IClosed2D> internalEdge2Ds = face2D.InternalEdge2Ds;
            //    if (internalEdge2Ds == null || internalEdge2Ds.Count == 0)
            //    {
            //        bool rectangular = Planar.Query.Rectangular(externalEdge2D, out Planar.Rectangle2D rectangle2D, tolerance);
            //        if (rectangular)
            //        {

            //        }
            //    }
            //}

            //return null;
        }
    }
}
