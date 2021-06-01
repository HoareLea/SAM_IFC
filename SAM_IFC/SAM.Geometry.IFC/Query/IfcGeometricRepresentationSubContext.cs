using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Geometry.IFC
{
    public static partial class Query
    {
        public static IfcGeometricRepresentationSubContext IfcGeometricRepresentationSubContext(this IfcProject ifcProject, IfcDefaultContextIdentifier ifcDefaultContextIdentifier)
        {
            if (ifcDefaultContextIdentifier == IfcDefaultContextIdentifier.Undefined || ifcProject == null)
            {
                return null;
            }

            string contextIdentifier = Core.Query.Description(ifcDefaultContextIdentifier);
            if (string.IsNullOrWhiteSpace(contextIdentifier))
            {
                return null;
            }


            IEnumerable<IfcGeometricRepresentationSubContext> ifcGeometricRepresentationSubContexts = ifcProject.Extract<IfcGeometricRepresentationSubContext>();
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

            IfcProject ifcProject = ifcGeometricRepresentationContext.Database?.Project;
            if(ifcProject == null)
            {
                return null;
            }

            IEnumerable<IfcGeometricRepresentationSubContext> ifcGeometricRepresentationSubContexts = ifcProject.Extract<IfcGeometricRepresentationSubContext>();
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

                if(ifcGeometricRepresentationContext_Parent.Index != ifcGeometricRepresentationContext.Index)
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