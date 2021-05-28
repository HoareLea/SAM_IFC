using System.Linq;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcCurtainWall ToIFC_IfcCurtainWall(this Panel panel, Xbim.Common.IModel model)
        {
            if(panel == null || model == null)
            {
                return null;
            }

            IfcCurtainWall result = model.Instances.New<IfcCurtainWall>();
            result.SetIfcProductRepresentation(panel);
            Core.IFC.Modify.SetIfcPropertySets(result, panel);

            return result;
        }
    }
}
