namespace SAM.Core.IFC
{
    public static partial class Query
    {
        public static string Description(this SAMObject sAMObject)
        {
            if(sAMObject == null)
            {
                return null;
            }

            string name = sAMObject.Name;
            if (string.IsNullOrWhiteSpace(name))
                return string.Format("[{0}]", sAMObject.Guid);
            else
                return string.Format("{0} [{1}]", name, sAMObject.Guid);
        }
    }
}
