using System.ComponentModel;

namespace SAM.Geometry.IFC
{
    [Description("Ifc Default Context Type")]
    public enum IfcDefaultContextType
    {
        [Description("Undefined")] Undefined,
        [Description("Curve2D")] Curve2D,
        [Description("SweptSolid")] SweptSolid,
        [Description("Brep")] Brep,
    }
}