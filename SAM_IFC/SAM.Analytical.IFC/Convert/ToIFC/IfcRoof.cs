using GeometryGym.Ifc;
using System.Linq;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcRoof ToIFC_IfcRoof(this Panel panel, IfcObjectDefinition host, double tolerance = Core.Tolerance.Distance)
        {
            if(panel == null || host == null)
            {
                return null;
            }

            Geometry.Spatial.Plane plane = panel.Plane;

            IfcRoof result = new IfcRoof(host, 
                Geometry.IFC.Create.IfcLocalPlacement(host.Database, plane),
                Create.IfcProductDefinitionShape(host.Database, panel, tolerance));

            result.SetIfcBuildingElement(panel);
            Core.IFC.Modify.SetIfcPropertySets(result, panel);

            return result;
        }
    }
}
