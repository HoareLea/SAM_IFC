using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcArbitraryProfileDefWithVoids IfcArbitraryProfileDefWithVoids(this DatabaseIfc databaseIfc, Planar.Face2D face2D, IfcProfileTypeEnum ifcProfileTypeEnum)
        {
            if(face2D == null || databaseIfc == null)
            {
                return null;
            }

            List<IfcCurve> ifcCurves = new List<IfcCurve>();

            List<Planar.IClosed2D> internalEdges = face2D.InternalEdge2Ds;
            if(internalEdges != null)
            {
                foreach(Planar.IClosed2D internalEdge in internalEdges)
                {
                    ifcCurves.Add((internalEdge as dynamic).ToIFC(databaseIfc));
                }
            }

            IfcArbitraryProfileDefWithVoids result = new IfcArbitraryProfileDefWithVoids(string.Empty, (face2D.ExternalEdge2D as dynamic).ToIFC(databaseIfc), ifcCurves);
            result.ProfileType = ifcProfileTypeEnum;

            return result;
        }
    }
}
