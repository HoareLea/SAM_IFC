using GeometryGym.Ifc;
using System.Collections.Generic;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcShapeRepresentation IfcShapeRepresentation(this DatabaseIfc databaseIfc, IfcRepresentationItem ifcRepresentationItem, IfcGeometricRepresentationSubContext.SubContextIdentifier subContextIdentifier, ShapeRepresentationType shapeRepresentationType)
        {
            if(databaseIfc == null || ifcRepresentationItem == null)
            {
                return null;
            }

            IfcGeometricRepresentationSubContext ifcGeometricRepresentationSubContext = databaseIfc.Factory.SubContext(subContextIdentifier);

            IfcShapeRepresentation result = new IfcShapeRepresentation(ifcGeometricRepresentationSubContext, new List<IfcRepresentationItem>() { ifcRepresentationItem }, shapeRepresentationType);
            result.Items.Add(ifcRepresentationItem);

            return result;
        }
    }
}
