using Xbim.Ifc4.MeasureResource;

namespace SAM.Core.IFC
{
    public static partial class Create
    {
        public static IfcBoolean IfcBoolean(this bool value)
        {
            return new IfcBoolean(value);
        }
    }
}
