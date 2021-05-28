using Xbim.Ifc4.MeasureResource;
namespace SAM.Core.IFC
{
    public static partial class Create
    {
        public static IfcInteger IfcInteger(this int value)
        {
            return new IfcInteger(value);
        }
    }
}
