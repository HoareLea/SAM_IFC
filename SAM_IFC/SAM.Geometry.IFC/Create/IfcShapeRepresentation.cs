using GeometryGym.Ifc;
using System.Collections.Generic;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcShapeRepresentation IfcShapeRepresentation(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, IfcRepresentationItem ifcRepresentationItem, IfcDefaultContextIdentifier ifcDefaultContextIdentifier, ShapeRepresentationType shapeRepresentationType)
        {
            if(ifcGeometricRepresentationContext == null || ifcRepresentationItem == null || ifcDefaultContextIdentifier == IfcDefaultContextIdentifier.Undefined)
            {
                return null;
            }

            DatabaseIfc databaseIfc = ifcGeometricRepresentationContext.Database;
            if (databaseIfc == null)
            {
                return null;
            }

            IfcGeometricRepresentationSubContext ifcGeometricRepresentationSubContext = Query.IfcGeometricRepresentationSubContext(ifcGeometricRepresentationContext, ifcDefaultContextIdentifier);

            IfcShapeRepresentation result = new IfcShapeRepresentation(ifcGeometricRepresentationSubContext, new List<IfcRepresentationItem>() { ifcRepresentationItem }, shapeRepresentationType);
            result.ContextOfItems = ifcGeometricRepresentationSubContext != null ? ifcGeometricRepresentationSubContext : ifcGeometricRepresentationContext;
            result.RepresentationIdentifier = Core.Query.Description(ifcDefaultContextIdentifier);
            result.Items.Add(ifcRepresentationItem);

            return result;
        }
    }
}
