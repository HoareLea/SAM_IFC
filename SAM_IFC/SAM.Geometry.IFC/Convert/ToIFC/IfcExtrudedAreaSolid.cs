using SAM.Geometry.Spatial;
using Xbim.Ifc4.GeometricModelResource;

namespace SAM.Geometry.IFC
{
    public static partial class Convert
    {
        public static IfcExtrudedAreaSolid ToIFC(this Extrusion extrusion, Xbim.Common.IModel model)
        {
            if(extrusion == null || model == null)
            {
                return null;
            }

            Face3D face3D = extrusion.Face3D;
            if(face3D == null)
            {
                return null;
            }

            Plane plane = face3D.GetPlane();
            if(plane == null)
            {
                return null;
            }

            Planar.Face2D face2D = plane.Convert(face3D);
            if (face2D == null)
            {
                return null;
            }

            IfcExtrudedAreaSolid result = model.Instances.New<IfcExtrudedAreaSolid>();
            result.Depth = extrusion.Vector.Length;
            result.ExtrudedDirection = extrusion.Vector.GetNormalized().ToIFC(model);
            result.SweptArea = Create.IfcProfileDef(model, face2D, Xbim.Ifc4.Interfaces.IfcProfileTypeEnum.AREA);

            return result;
        }
    }
}
