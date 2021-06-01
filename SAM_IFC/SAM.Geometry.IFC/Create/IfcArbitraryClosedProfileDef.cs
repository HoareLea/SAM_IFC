using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcArbitraryClosedProfileDef IfcArbitraryClosedProfileDef(this DatabaseIfc databaseIfc, Planar.Polygon2D polygon2D, IfcProfileTypeEnum ifcProfileTypeEnum)
        {
            if(polygon2D == null || databaseIfc == null)
            {
                return null;
            }

            IfcArbitraryClosedProfileDef result = new IfcArbitraryClosedProfileDef(string.Empty, polygon2D.ToIFC_IfcPolyline(databaseIfc));
            result.ProfileType = ifcProfileTypeEnum;

            return result;
        }
    }
}
