using GeometryGym.Ifc;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcBuilding ToIFC_IfcBuilding(this AnalyticalModel analyticalModel, DatabaseIfc databaseIfc)
        {
            if(analyticalModel == null || databaseIfc == null)
            {
                return null;
            }

            IfcBuilding result = new IfcBuilding(databaseIfc, analyticalModel.Name);
            result.CompositionType = IfcElementCompositionEnum.ELEMENT;

            Geometry.Spatial.Point3D point3D = Geometry.Spatial.Create.Point3D(0, 0, 0);
            IfcLocalPlacement ifcLocalPlacement = Geometry.IFC.Create.IfcLocalPlacement(databaseIfc, point3D);
            result.ObjectPlacement = ifcLocalPlacement;

            return result;
        }
    }
}
