using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.MeasureResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcCartesianPointList3D ToIFC_IfcCartesianPointList3D(this IEnumerable<Spatial.Point3D> point3Ds, Xbim.Common.IModel model)
        {
            if(point3Ds == null || model == null)
            {
                return null;
            }

            IfcCartesianPointList3D result = model.Instances.New<IfcCartesianPointList3D>();
            for(int i=0; i < point3Ds.Count(); i++)
            {
                Spatial.Point3D point3D = point3Ds.ElementAt(i);
                if(point3D != null)
                {
                    IItemSet<IfcLengthMeasure> ifcLengthMeasures = result.CoordList.GetAt(i);
                    ifcLengthMeasures.Add(point3D[0]);
                    ifcLengthMeasures.Add(point3D[1]);
                    ifcLengthMeasures.Add(point3D[2]);
                }
            }

            return result;
        }
    }
}
