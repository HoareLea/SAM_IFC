using GH_IO.Serialization;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SAM.Core.Grasshopper.IFC.Properties;
using System;
using System.Collections.Generic;
using GeometryGym.Ifc;

namespace SAM.Core.Grasshopper.IFC
{
    public class GooDatabaseIfc : GH_Goo<DatabaseIfc>
    {
        public GooDatabaseIfc()
            : base()
        {
        }

        public GooDatabaseIfc(DatabaseIfc databaseIfc)
        {
            Value = databaseIfc;
        }

        public override bool IsValid => Value != null;

        public override string TypeName => typeof(DatabaseIfc).Name;

        public override string TypeDescription => typeof(DatabaseIfc).FullName.Replace(".", " ");

        public override IGH_Goo Duplicate()
        {
            return new GooDatabaseIfc(Value);
        }

        public override bool Write(GH_IWriter writer)
        {
            if (Value == null)
                return false;

            string path = System.IO.Path.GetTempFileName();
            string ifc = null;
            try
            {
                Value.WriteFile(path);
                if(System.IO.File.Exists(path))
                {
                    ifc = System.IO.File.ReadAllText(path);
                }

            }
            catch
            {
                ifc = null;
            }
            finally
            {
                if(!string.IsNullOrWhiteSpace(path) && System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            
            if(ifc != null)
            {
                writer.SetString(typeof(DatabaseIfc).FullName, ifc);
                return true;
            }

            return false;
        }

        public override bool Read(GH_IReader reader)
        {
            string value = null;
            if (!reader.TryGetString(typeof(DatabaseIfc).FullName, ref value))
                return false;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            bool result = false;

            string path = System.IO.Path.GetTempFileName();
            try
            {
                System.IO.File.WriteAllText(path, value);
                if (System.IO.File.Exists(path))
                {
                    Value = new DatabaseIfc(path);
                    result = true;
                }

            }
            catch
            {
                value = null;
                result = false;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(path) && System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return result;
        }

        public override string ToString()
        {
            string value = typeof(DatabaseIfc).FullName;

            return value;
        }

        public override bool CastFrom(object source)
        {
            if (source is DatabaseIfc)
            {
                Value = (DatabaseIfc)source;
                return true;
            }

            return base.CastFrom(source);
        }

        public override bool CastTo<Y>(ref Y target)
        {
            if (typeof(Y) == typeof(DatabaseIfc))
            {
                target = (Y)(object)Value;
                return true;
            }

            if (typeof(Y) == typeof(object))
            {
                target = (Y)(object)Value;
                return true;
            }

            return base.CastTo<Y>(ref target);
        }
    }

    public class GooDatabaseIfcParam : GH_PersistentParam<GooDatabaseIfc>
    {
        public override Guid ComponentGuid => new Guid("407388bf-2655-4c3d-990b-eb8a64b46c74");
        protected override System.Drawing.Bitmap Icon => Resources.SAM_Small;

        public GooDatabaseIfcParam()
            : base(typeof(GooDatabaseIfc).Name, typeof(GooDatabaseIfc).Name, typeof(GooDatabaseIfc).FullName.Replace(".", " "), "Params", "SAM")
        {
        }

        protected override GH_GetterResult Prompt_Plural(ref List<GooDatabaseIfc> values)
        {
            throw new NotImplementedException();
        }

        protected override GH_GetterResult Prompt_Singular(ref GooDatabaseIfc value)
        {
            throw new NotImplementedException();
        }
    }
}