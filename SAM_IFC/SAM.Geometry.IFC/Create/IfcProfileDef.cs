using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProfileResource;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcProfileDef IfcProfileDef(this Xbim.Common.IModel model, Planar.Face2D face2D, IfcProfileTypeEnum ifcProfileTypeEnum, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.Distance)
        {
            if(face2D == null || model == null)
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

                bool rectangular = Planar.Query.Rectangular(externalEdge2D, out Planar.Rectangle2D rectangle2D, tolerance_Distance);
                if (rectangular)
                {
                    return IfcRectangleProfileDef(model, rectangle2D, ifcProfileTypeEnum);
                }
                else
                {
                    return IfcArbitraryClosedProfileDef(model, externalEdge2D as dynamic, ifcProfileTypeEnum);
                }
            }
            else
            {
                return IfcArbitraryProfileDefWithVoids(model, face2D, ifcProfileTypeEnum);
            }
        }
    }
}
