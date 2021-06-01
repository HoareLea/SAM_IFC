using GeometryGym.Ifc;
using System.Linq;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcSlab ToIFC_IfcSlab(this Panel panel, IfcObjectDefinition host, double tolerance = Core.Tolerance.Distance)
        {
            if(panel == null || host == null)
            {
                return null;
            }

            Geometry.Spatial.Plane plane = panel.Plane;

            IfcGeometricRepresentationContext ifcGeometricRepresentationContext = host.Database?.Project?.Extract<IfcGeometricRepresentationContext>().FirstOrDefault();

            IfcSlab result = new IfcSlab(host,
                Geometry.IFC.Create.IfcLocalPlacement(host.Database, plane),
                Create.IfcProductDefinitionShape(ifcGeometricRepresentationContext, panel, tolerance));

            result.SetIfcBuildingElement(panel);
            Core.IFC.Modify.SetIfcPropertySets(result, panel);

            return result;
        }
    }
}
