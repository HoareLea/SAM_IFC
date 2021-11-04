using SAM.Geometry.Spatial;
using SAM.Geometry.Planar;
using System.Collections.Generic;

namespace SAM.Geometry.IFC
{
    public static partial class Query
    {
        public static IClosed2D Simplify(IClosed2D closed2D, double tolerance_Angle = Core.Tolerance.Angle)
        {
            if(closed2D is Polygon2D)
            {
                Polygon2D polygon2D = (Polygon2D)closed2D;
                polygon2D = Planar.Query.SimplifyByAngle(polygon2D, tolerance_Angle);
                return polygon2D;
            }

            return closed2D.Clone() as IClosed2D;
        }
        
        public static Face3D Simplify(this Face3D face3D)
        {
            if(face3D == null)
            {
                return null;
            }

            Plane plane = face3D.GetPlane();
            if(plane == null)
            {
                return new Face3D(face3D);
            }

            IClosed2D externalEdge2D = Simplify(face3D.ExternalEdge2D);
            if(externalEdge2D == null)
            {
                return new Face3D(face3D);
            }
            
            
            List<IClosed2D> internalEdge2Ds = face3D.InternalEdge2Ds;
            if(internalEdge2Ds != null && internalEdge2Ds.Count != 0)
            {
                internalEdge2Ds = internalEdge2Ds.ConvertAll(x => Simplify(x));
            }


            return new Face3D(plane, Planar.Create.Face2D(externalEdge2D, internalEdge2Ds));
        }

        public static Shell Simplify(this Shell shell)
        {
            if(shell == null)
            {
                return null;
            }

            return new Shell(shell.Face3Ds?.ConvertAll(x => Simplify(x)));
        }
    }
}