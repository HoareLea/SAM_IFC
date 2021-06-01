using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcProfileDef IfcProfileDef(this DatabaseIfc databaseIfc, Planar.Face2D face2D, IfcProfileTypeEnum ifcProfileTypeEnum, double tolerance = Core.Tolerance.Distance)
        {
            if(face2D == null || databaseIfc == null)
            {
                return null;
            }

            List<Planar.IClosed2D> internalEdge2Ds = face2D.InternalEdge2Ds;
            if(internalEdge2Ds == null || internalEdge2Ds.Count == 0)
            {
                Planar.IClosed2D externalEdge2D = face2D.ExternalEdge2D;
                if(externalEdge2D == null)
                {
                    return null;
                }

                bool rectangular = Planar.Query.Rectangular(externalEdge2D, out Planar.Rectangle2D rectangle2D, tolerance);
                if (rectangular)
                {
                    return IfcRectangleProfileDef(databaseIfc, rectangle2D, ifcProfileTypeEnum);
                }
                else
                {
                    return IfcArbitraryClosedProfileDef(databaseIfc, externalEdge2D as dynamic, ifcProfileTypeEnum);
                }
            }
            else
            {
                return IfcArbitraryProfileDefWithVoids(databaseIfc, face2D, ifcProfileTypeEnum);
            }
        }
    }
}
