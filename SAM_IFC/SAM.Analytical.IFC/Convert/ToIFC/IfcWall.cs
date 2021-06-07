using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcWall ToIFC_IfcWall(this Panel panel, Xbim.Common.IModel model)
        {
            if(panel == null || model == null)
            {
                return null;
            }

            IfcWall result = model.Instances.New<IfcWall>();
            result.SetIfcBuildingElement(panel);
            result.SetIfcProductRepresentation(panel);
            Modify.SetIfcPropertySets(result, panel);

            return result;
        }
    }
}
