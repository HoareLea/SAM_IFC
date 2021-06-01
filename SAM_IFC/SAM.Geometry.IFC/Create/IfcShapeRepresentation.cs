using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcShapeRepresentation IfcShapeRepresentation(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, IfcRepresentationItem ifcRepresentationItem, string representationType, string representationIdentifier)
        {
            if(ifcGeometricRepresentationContext == null || ifcRepresentationItem == null || string.IsNullOrWhiteSpace(representationType) || string.IsNullOrWhiteSpace(representationIdentifier))
            {
                return null;
            }

            Xbim.Common.IModel model = ifcGeometricRepresentationContext.Model;
            if (model == null)
            {
                return null;
            }

            IfcShapeRepresentation result = model.Instances.New<IfcShapeRepresentation>();
            result.ContextOfItems = ifcGeometricRepresentationContext;
            result.RepresentationType = representationType;
            result.RepresentationIdentifier = representationIdentifier;
            result.Items.Add(ifcRepresentationItem);

            return result;
        }
    }
}
