using GeometryGym.Ifc;

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

            DatabaseIfc databaseIfc = ifcGeometricRepresentationContext.Database;
            if(databaseIfc == null)
            {
                return null;
            }

            IfcGeometricRepresentationSubContext result = new IfcGeometricRepresentationSubContext(ifcGeometricRepresentationContext, IfcGeometricProjectionEnum.MODEL_VIEW);
            result.ParentContext = ifcGeometricRepresentationContext;
            result.ContextIdentifier = Core.Query.Description(ifcDefaultContextIdentifier);
            result.ContextType = ifcGeometricRepresentationContext.ContextType;

            return result;
        }
    }
}
