using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcWall ToIFC_IfcWall(this Wall wall, Xbim.Common.IModel model, BuildingModel buildingModel)
        {
            if(wall == null || model == null)
            {
                return null;
            }

            IfcWall result = model.Instances.New<IfcWall>();
            result.SetIfcBuildingElement(wall);
            result.SetIfcProductRepresentation(wall);

            if (buildingModel != null)
            {
                Modify.SetIfcPropertySets(result, wall, buildingModel);
            }

            return result;
        }
    }
}
