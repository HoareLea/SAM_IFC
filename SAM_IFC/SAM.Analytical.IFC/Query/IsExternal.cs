using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Query
    {
        public static bool? IsExternal(this IfcWall ifcWall)
        {
            IfcBoolean? ifcBoolean = ifcWall?.GetPropertySingleValue<IfcBoolean>("Pset_WallCommon", "IsExternal");
            if(ifcBoolean == null || !ifcBoolean.HasValue)
            {
                return null;
            }    

            return ifcBoolean.Value;
        }

        public static bool? IsExternal(this IfcRoof ifcRoof)
        {
            IfcBoolean? ifcBoolean = ifcRoof?.GetPropertySingleValue<IfcBoolean>("Pset_RoofCommon", "IsExternal");
            if (ifcBoolean == null || !ifcBoolean.HasValue)
            {
                return null;
            }

            return ifcBoolean.Value;
        }

        public static bool? IsExternal(this IfcWindow ifcWindow)
        {
            IfcBoolean? ifcBoolean = ifcWindow?.GetPropertySingleValue<IfcBoolean>("Pset_WindowCommon", "IsExternal");
            if (ifcBoolean == null || !ifcBoolean.HasValue)
            {
                return null;
            }

            return ifcBoolean.Value;
        }

        public static bool? IsExternal(this IfcSlab ifcSlab)
        {
            IfcBoolean? ifcBoolean = ifcSlab?.GetPropertySingleValue<IfcBoolean>("Pset_SlabCommon", "IsExternal");
            if (ifcBoolean == null || !ifcBoolean.HasValue)
            {
                return null;
            }

            return ifcBoolean.Value;
        }

        public static bool? IsExternal(this IfcCurtainWall ifcCurtainWall)
        {
            IfcBoolean? ifcBoolean = ifcCurtainWall?.GetPropertySingleValue<IfcBoolean>("Pset_CurtainWallCommon", "IsExternal");
            if (ifcBoolean == null || !ifcBoolean.HasValue)
            {
                return null;
            }

            return ifcBoolean.Value;
        }
    }
}
