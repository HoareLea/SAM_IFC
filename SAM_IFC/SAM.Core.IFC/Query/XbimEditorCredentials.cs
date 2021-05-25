using System;
using Xbim.Ifc;

namespace SAM.Core.IFC
{
    public static partial class Query
    {
        public static XbimEditorCredentials XbimEditorCredentials()
        {
            XbimEditorCredentials result = new XbimEditorCredentials
            {
                ApplicationDevelopersName = "SAM",
                ApplicationFullName = "Single Analysis Model",
                ApplicationIdentifier = "SAM",
                ApplicationVersion = "1.0",
                EditorsFamilyName = string.Empty,
                EditorsGivenName = Environment.UserName,
                EditorsOrganisationName = string.Empty
            };

            return result;
        }
    }
}
