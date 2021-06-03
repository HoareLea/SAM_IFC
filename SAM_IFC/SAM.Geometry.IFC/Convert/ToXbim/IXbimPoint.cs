using Microsoft.Extensions.Logging;
using Xbim.Common.Geometry;
using Xbim.Geometry.Engine.Interop;
using Xbim.Ifc4.GeometryResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IXbimPoint ToXbim(this IfcCartesianPoint ifcCartesianPoint, XbimGeometryEngine xbimGeometryEngine)
        {
            if(ifcCartesianPoint == null || xbimGeometryEngine == null)
            {
                return null;
            }

            return xbimGeometryEngine.CreatePoint(ifcCartesianPoint);
        }
    }
}
