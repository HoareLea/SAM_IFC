using GeometryGym.Ifc;

namespace SAM.Core.IFC
{
    public static partial class Create
    {
        public static IfcText IfcText(this string value)
        {
            if(value == null)
            {
                return null;
            }

            return new IfcText(value);
        }
    }
}
