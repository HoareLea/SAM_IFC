using Xbim.Ifc4.RepresentationResource;

namespace SAM.Geometry.IFC
{
    public static partial class Create
    {
        public static IfcGeometricRepresentationSubContext IfcGeometricRepresentationSubContext(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, IfcDefaultContextIdentifier ifcDefaultContextIdentifier)
        {
            if (ifcGeometricRepresentationContext == null || ifcDefaultContextIdentifier == IfcDefaultContextIdentifier.Undefined)
            {
                return null;
            }

            Xbim.Common.IModel model = ifcGeometricRepresentationContext.Model;
            if(model == null)
            {
                return null;
            }

            IfcGeometricRepresentationSubContext result = model.Instances.New<IfcGeometricRepresentationSubContext>();
            result.ParentContext = ifcGeometricRepresentationContext;
            result.ContextIdentifier = Core.Query.Description(ifcDefaultContextIdentifier);
            result.ContextType = ifcGeometricRepresentationContext.ContextType;
            result.TargetView = Xbim.Ifc4.Interfaces.IfcGeometricProjectionEnum.MODEL_VIEW;

            return result;
        }
    }
}
