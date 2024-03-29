﻿using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcBuilding ToIFC_IfcBuilding(this BuildingModel buildingModel, Xbim.Common.IModel model)
        {
            if(buildingModel == null || model == null)
            {
                return null;
            }

            IfcBuilding result = model.Instances.New<IfcBuilding>();
            result.Name = buildingModel.Name;
            result.CompositionType = Xbim.Ifc4.Interfaces.IfcElementCompositionEnum.ELEMENT;

            Geometry.Spatial.Point3D point3D = Geometry.Spatial.Create.Point3D(0, 0, 0);
            Xbim.Ifc4.GeometricConstraintResource.IfcLocalPlacement ifcLocalPlacement = Geometry.IFC.Create.IfcLocalPlacement(model, point3D);
            result.ObjectPlacement = ifcLocalPlacement;

            return result;
        }
    }
}
