using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcCurtainWall ToIFC_IfcCurtainWall(this Wall wall, Xbim.Common.IModel model)
        {
            if(wall == null || model == null)
            {
                return null;
            }

            IfcCurtainWall result = model.Instances.New<IfcCurtainWall>();
            result.SetIfcBuildingElement(wall);
            result.SetIfcProductRepresentation(wall);
            Core.IFC.Modify.SetIfcPropertySets(result, wall);

            return result;
        }
    }
}
