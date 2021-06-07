using System.Linq;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.PropertyResource;
using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Modify
    {
        public static IIfcPropertySingleValue SetIsExternal(this IfcBuildingElement ifcBuildingElement, Panel panel, AdjacencyCluster adjacencyCluster)
        {
            if (ifcBuildingElement == null || panel == null)
            {
                return null;
            }

            if (adjacencyCluster != null)
            {
                return SetIsExternal(ifcBuildingElement as dynamic, adjacencyCluster.External(panel));
            }

            return SetIsExternal(ifcBuildingElement as dynamic, Analytical.Query.External(panel.PanelType));
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcBuildingElementType ifcBuildingElementType, PanelType panelType)
        {
            return SetIsExternal(ifcBuildingElementType as dynamic, Analytical.Query.External(panelType));
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcWall ifcWall, bool value)
        {
            return ifcWall?.SetPropertySingleValue("Pset_WallCommon", "IsExternal", new IfcBoolean(value));
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcWallType ifcWallType, bool value)
        {
            return SetIsExternal(ifcWallType, "Pset_WallCommon", "IsExternal", value);
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcRoof ifcRoof, bool value)
        {
            return ifcRoof?.SetPropertySingleValue("Pset_RoofCommon", "IsExternal", new IfcBoolean(value));
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcRoofType ifcRoofType, bool value)
        {
            return SetIsExternal(ifcRoofType, "Pset_RoofCommon", "IsExternal", value);
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcSlab ifcSlab, bool value)
        {
            return ifcSlab?.SetPropertySingleValue("Pset_SlabCommon", "IsExternal", new IfcBoolean(value));
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcSlabType ifcSlabType, bool value)
        {
            return SetIsExternal(ifcSlabType, "Pset_SlabCommon", "IsExternal", value);
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcCurtainWall ifcCurtainWall, bool value)
        {
            return ifcCurtainWall?.SetPropertySingleValue("Pset_CurtainWallCommon", "IsExternal", new IfcBoolean(value));
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcCurtainWallType ifcCurtainWallType, bool value)
        {
            return SetIsExternal(ifcCurtainWallType, "Pset_CurtainWallCommon", "IsExternal", value);
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcWindow ifcWindow, bool value)
        {
            return ifcWindow?.SetPropertySingleValue("Pset_WindowCommon", "IsExternal", new IfcBoolean(value));
        }

        public static IIfcPropertySingleValue SetIsExternal(this IfcWindowType ifcWindowType, bool value)
        {
            return SetIsExternal(ifcWindowType, "Pset_WindowCommon", "IsExternal", value);
        }

        private static IIfcPropertySingleValue SetIsExternal(this IfcBuildingElementType ifcBuildingElementType, string propertySetName, string propertyName, bool value)
        {
            Xbim.Common.IModel model = ifcBuildingElementType?.Model;
            if (model == null)
            {
                return null;
            }

            IfcPropertySet ifcPropertySet = ifcBuildingElementType.HasPropertySets.Where<IfcPropertySet>(x => x is IfcPropertySet && propertySetName.Equals(x.Name)).FirstOrDefault();
            if (ifcPropertySet == null)
            {
                ifcPropertySet = model.Instances.New<IfcPropertySet>();
                ifcPropertySet.Name = propertySetName;
                ifcBuildingElementType.HasPropertySets.Add(ifcPropertySet);
            }

            IfcPropertySingleValue ifcPropertySingleValue = ifcPropertySet.HasProperties.Where<IfcPropertySingleValue>(x => propertyName.Equals(x.Name)).FirstOrDefault();
            if (ifcPropertySingleValue == null)
            {
                ifcPropertySingleValue = model.Instances.New<IfcPropertySingleValue>();
                ifcPropertySingleValue.Name = propertyName;
                ifcPropertySet.HasProperties.Add(ifcPropertySingleValue);
            }

            ifcPropertySingleValue.NominalValue = new IfcBoolean(value);
            return ifcPropertySingleValue;
        }
    }
}
