using GeometryGym.Ifc;

namespace SAM.Core.IFC
{
    public static partial class Create
    {
        public static IfcReal IfcReal(this double value)
        {
            return new IfcReal(value);
        }
    }
}
