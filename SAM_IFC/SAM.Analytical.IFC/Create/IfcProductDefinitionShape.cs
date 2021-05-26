using SAM.Geometry.Spatial;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.RepresentationResource;

namespace SAM.Analytical.IFC
{
    public static partial class Create
    {
        public static IfcProductDefinitionShape IfcProductDefinitionShape(this IfcGeometricRepresentationContext ifcGeometricRepresentationContext, Panel panel)
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

            double thickness = 0;

            Extrusion extrusion = null;

            Construction construction = panel.Construction;
            if (construction != null)
            {
                thickness = construction.GetThickness();
            }

            if (!double.IsNaN(thickness) && thickness != 0)
            {
                Plane plane = face3D.GetPlane();
                Vector3D vector3D = plane.Normal * (thickness / 2);

                BoundingBox3D boundingBox3D = face3D.GetBoundingBox();
                if (boundingBox3D != null)
                {

                }

            }

            //Implement Conversion Panel to Extrusion
            throw new System.NotImplementedException();

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
