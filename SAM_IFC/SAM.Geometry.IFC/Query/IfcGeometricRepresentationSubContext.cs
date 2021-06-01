using System.Collections.Generic;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Geometry.IFC
{
    public static partial class Query
    {
        public static IfcGeometricRepresentationSubContext IfcGeometricRepresentationSubContext(this Xbim.Common.IModel model, IfcDefaultContextIdentifier ifcDefaultContextIdentifier)
        {
            if (ifcDefaultContextIdentifier == IfcDefaultContextIdentifier.Undefined || model == null)
            {
                return null;
            }

            string contextIdentifier = Core.Query.Description(ifcDefaultContextIdentifier);
            if (string.IsNullOrWhiteSpace(contextIdentifier))
            {
                return null;
            }


            IEnumerable<IfcGeometricRepresentationSubContext> ifcGeometricRepresentationSubContexts = model.Instances.OfType<IfcGeometricRepresentationSubContext>();
            if(ifcGeometricRepresentationSubContexts == null)
            {
                return null;
            }

            foreach(IfcGeometricRepresentationSubContext ifcGeometricRepresentationSubContext in ifcGeometricRepresentationSubContexts)
            {
                if(ifcGeometricRepresentationSubContext == null)
                {
                    continue;
                }

                if(contextIdentifier.Equals( ifcGeometricRepresentationSubContext.ContextIdentifier))
                {
                    return ifcGeometricRepresentationSubContext;
                }
            }

            return null;
        }

        public static IfcGeometricRepresentationSubContext IfcGeometricRepresentationSubContext(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, IfcDefaultContextIdentifier ifcDefaultContextIdentifier)
        {
            if (ifcDefaultContextIdentifier == IfcDefaultContextIdentifier.Undefined || ifcGeometricRepresentationContext == null)
            {
                return null;
            }

            string contextIdentifier = Core.Query.Description(ifcDefaultContextIdentifier);
            if (string.IsNullOrWhiteSpace(contextIdentifier))
            {
                return null;
            }

            Xbim.Common.IModel model = ifcGeometricRepresentationContext.Model;
            if(model == null)
            {
                return null;
            }

            IEnumerable<IfcGeometricRepresentationSubContext> ifcGeometricRepresentationSubContexts = model.Instances.OfType<IfcGeometricRepresentationSubContext>();
            if (ifcGeometricRepresentationSubContexts == null)
            {
                return null;
            }

            foreach (IfcGeometricRepresentationSubContext ifcGeometricRepresentationSubContext in ifcGeometricRepresentationSubContexts)
            {
                if (ifcGeometricRepresentationSubContext == null)
                {
                    continue;
                }

                IfcGeometricRepresentationContext ifcGeometricRepresentationContext_Parent = ifcGeometricRepresentationSubContext.ParentContext;
                if (ifcGeometricRepresentationContext_Parent == null)
                {
                    continue;
                }

                if(ifcGeometricRepresentationContext_Parent.EntityLabel != ifcGeometricRepresentationContext.EntityLabel)
                {
                    continue;
                }

                if (contextIdentifier.Equals(ifcGeometricRepresentationSubContext.ContextIdentifier))
                {
                    return ifcGeometricRepresentationSubContext;
                }
            }

            return null;
        }
    }
}