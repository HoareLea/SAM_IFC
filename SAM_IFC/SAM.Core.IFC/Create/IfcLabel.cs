using Xbim.Ifc4.MeasureResource;

namespace SAM.Core.IFC
{
    public static partial class Create
    {
        public static IfcLabel IfcLabel(this string value)
        {
            if(value == null)
            {
                return null;
            }

            return new IfcLabel(value);
        }
    }
}
