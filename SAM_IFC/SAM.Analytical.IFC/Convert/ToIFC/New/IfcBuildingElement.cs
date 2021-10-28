using Xbim.Ifc4.ProductExtension;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcBuildingElement ToIFC(this IPartition partition, Xbim.Common.IModel model, ArchitecturalModel architecturalModel)
        {
            if (partition == null || model == null)
            {
                return null;
            }

            if(partition is IHostPartition)
            {
                IHostPartition hostPartition = (IHostPartition)partition;

                if(architecturalModel != null)
                {
                    if (architecturalModel.GetMaterialType(hostPartition) == Core.MaterialType.Transparent)
                    {
                        return ((Wall)hostPartition).ToIFC_IfcCurtainWall(model);
                    }
                }

                return ((Wall)hostPartition).ToIFC_IfcWall(model, architecturalModel);
            }

            if(partition is Floor)
            {
               return ((Floor)partition).ToIFC_IfcSlab(model);
            }

            if (partition is Roof)
            {
                return ((Roof)partition).ToIFC_IfcSlab(model);
            }

            return null;
        }
    }
}
