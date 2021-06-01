using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.MeasureResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcCartesianPointList2D ToIFC_IfcCartesianPointList2D(this IEnumerable<Planar.Point2D> point2Ds, IModel model)
        {
            if(point2Ds == null || model == null)
            {
                return null;
            }

            IfcCartesianPointList2D result = model.Instances.New<IfcCartesianPointList2D>();
            for(int i=0; i < point2Ds.Count(); i++)
            {
                Planar.Point2D point2D = point2Ds.ElementAt(i);
                if(point2D != null)
                {
                    IItemSet<IfcLengthMeasure> ifcLengthMeasures = result.CoordList.GetAt(i);
                    ifcLengthMeasures.Add(point2D[0]);
                    ifcLengthMeasures.Add(point2D[1]);
                }
            }

            return result;
        }
    }
}
