using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcShapeRepresentation IfcShapeRepresentation(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, IfcRepresentationItem ifcRepresentationItem, IfcDefaultContextIdentifier ifcDefaultContextIdentifier, IfcDefaultContextType ifcDefaultContextType)
        {
            if(ifcGeometricRepresentationContext == null || ifcRepresentationItem == null || ifcDefaultContextType == IfcDefaultContextType.Undefined || ifcDefaultContextIdentifier == IfcDefaultContextIdentifier.Undefined)
            {
                return null;
            }

            Xbim.Common.IModel model = ifcGeometricRepresentationContext.Model;
            if (model == null)
            {
                return null;
            }

            IfcGeometricRepresentationSubContext ifcGeometricRepresentationSubContext = Query.IfcGeometricRepresentationSubContext(ifcGeometricRepresentationContext, ifcDefaultContextIdentifier);

            IfcShapeRepresentation result = model.Instances.New<IfcShapeRepresentation>();
            result.ContextOfItems = ifcGeometricRepresentationSubContext != null ? ifcGeometricRepresentationSubContext : ifcGeometricRepresentationContext;
            result.RepresentationIdentifier = Core.Query.Description(ifcDefaultContextIdentifier);
            result.RepresentationType = Core.Query.Description(ifcDefaultContextType);
            result.Items.Add(ifcRepresentationItem);

            return result;
        }
    }
}
