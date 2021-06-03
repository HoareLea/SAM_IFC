using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProfileResource;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcArbitraryProfileDefWithVoids IfcArbitraryProfileDefWithVoids(this Xbim.Common.IModel model, Planar.Face2D face2D, IfcProfileTypeEnum ifcProfileTypeEnum)
        {
            if(face2D == null || model == null)
            {
                return null;
            }

            IfcArbitraryProfileDefWithVoids result = model.Instances.New<IfcArbitraryProfileDefWithVoids>();
            result.OuterCurve = (face2D.ExternalEdge2D as dynamic).ToIFC(model);

            List<Planar.IClosed2D> internalEdges = face2D.InternalEdge2Ds;
            if(internalEdges != null)
            {
                foreach(Planar.IClosed2D internalEdge in internalEdges)
                {
                    result.InnerCurves.Add((internalEdge as dynamic).ToIFC(model));
                }
            }

            result.ProfileType = ifcProfileTypeEnum;

            return result;
        }
    }
}
