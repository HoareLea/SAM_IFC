using SAM.Analytical;
using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.SharedBldgElements;

namespace SAM.Analytical.IFC
{
    public static partial class Convert
    {
        public static IfcStore ToIFC(this AnalyticalModel analyticalModel)
        {
            if(analyticalModel == null)
            {
                return null;
            }

            XbimEditorCredentials xbimEditorCredentials = Core.IFC.Query.XbimEditorCredentials();

            IfcStore result = IfcStore.Create(xbimEditorCredentials, Xbim.Common.Step21.XbimSchemaVersion.Ifc4, Xbim.IO.XbimStoreType.InMemoryModel);

            IfcProject ifcProject = null;
            using (ITransaction transaction = result.BeginTransaction("Create Project"))
            {
                ifcProject = analyticalModel.ToIFC(result);

                transaction.Commit();
            }

            ifcProject = result.Instances.OfType<IfcProject>().FirstOrDefault();
            if(ifcProject != null)
            {
                IfcBuilding ifcBuilding = null;
                using (ITransaction transaction = result.BeginTransaction("Create Building"))
                {
                    ifcBuilding = analyticalModel.ToIFC_IfcBuilding(result);
                    ifcProject.AddBuilding(ifcBuilding);

                    transaction.Commit();
                }

                

                ifcBuilding = result.Instances.OfType<IfcBuilding>().FirstOrDefault();
                if(ifcBuilding != null)
                {

                    List<Panel> panels = analyticalModel.GetPanels();

                    foreach(Panel panel in panels)
                    {
                        IfcWallStandardCase ifcWallStandardCase = panel.ToIFC(result);
                        ifcBuilding.AddElement(ifcWallStandardCase);

                    }

                }
            }

            return result;
        }
    }
}
