using SAM.Geometry.Spatial;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Analytical.IFC
{
    public static partial class Create
    {
        public static IfcProductDefinitionShape IfcProductDefinitionShape(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, Panel panel, double tolerance = Core.Tolerance.Distance)
        {
            Face3D face3D = panel?.GetFace3D();
            if(face3D == null)
            {
                return null;
            }

            Xbim.Common.IModel model = ifcGeometricRepresentationContext.Model;
            if (model == null)
            {
                return null;
            }

            Extrusion extrusion = Query.Extrusion(panel, tolerance);
            if(extrusion == null)
            {
                return null;
            }

            IfcExtrudedAreaSolid ifcExtrudedAreaSolid = Geometry.IFC.Convert.ToIFC(extrusion, model);

            IfcShapeRepresentation ifcShapeRepresentation = model.Instances.New<IfcShapeRepresentation>();
            ifcShapeRepresentation.ContextOfItems = ifcGeometricRepresentationContext;
            ifcShapeRepresentation.RepresentationType = "SweptSolid";
            ifcShapeRepresentation.RepresentationIdentifier = "Body";
            ifcShapeRepresentation.Items.Add(ifcExtrudedAreaSolid);

            IfcProductDefinitionShape result = model.Instances.New<IfcProductDefinitionShape>();
            result.Representations.Add(ifcShapeRepresentation);

            return result;
        }
    }
}
