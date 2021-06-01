using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcProject ToIFC(this AnalyticalModel analyticalModel, IfcBuilding ifcBuilding)
        {
            if(analyticalModel == null || ifcBuilding == null)
            {
                return null;
            }

            IfcProject result = new IfcProject(ifcBuilding, analyticalModel.Name, IfcUnitAssignment.Length.Metre);
            return result;
        }
    }
}
