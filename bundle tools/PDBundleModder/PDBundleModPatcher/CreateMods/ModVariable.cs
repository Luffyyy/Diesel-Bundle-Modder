using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDBundleModPatcher
{   
    public class ModVariable
    {

        public object var { get; set; }
        public string name { get; set; }
        public string displayname { get; set; }
        public int displayposition = -1;

        public ModVariable(string type, string name, string defaultval, string displayname)
        {
            this.var = this.createVariableFromString(type, defaultval);
            this.name = name;
            this.displayname = displayname;
        }

        public ModVariable(string type, string name, string defaultval)
        {
            this.var = this.createVariableFromString(type, defaultval);
            this.name = name;
        }

        public ModVariable(object var, string name, string displayname)
        {
            this.var = var;
            this.name = name;
            this.displayname = displayname;
        }

        public ModVariable(object var, string name)
        {
            this.var = var;
            this.name = name;
        }

        public override bool Equals(object obj)
        {
            ModVariable q = obj as ModVariable;
            return q != null && q.name == this.name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public string getVariableValueString()
        {
            if (this.var is Boolean)
                return ((bool)this.var).ToString();
            else if (this.var is String)
                return ((string)this.var);
            else if (this.var is float)
                return ((float)this.var).ToString();
            else if (this.var is double)
                return ((double)this.var).ToString();
            else if (this.var is Int16)
                return ((Int16)this.var).ToString();
            else if (this.var is UInt16)
                return ((UInt16)this.var).ToString();
            else if (this.var is Int32)
                return ((Int32)this.var).ToString();
            else if (this.var is UInt32)
                return ((UInt32)this.var).ToString();
            else if (this.var is Int64)
                return ((Int64)this.var).ToString();
            else if (this.var is UInt64)
                return ((UInt64)this.var).ToString();
            else
                return "";
        }

        public string getVariableTypeString()
        {
            if (this.var is Boolean)
                return "bool";
            else if (this.var is String)
                return "string";
            else if (this.var is float)
                return "float";
            else if (this.var is double)
                return "double";
            else if (this.var is Int16)
                return "int16";
            else if (this.var is UInt16)
                return "uint16";
            else if (this.var is Int32)
                return "int32";
            else if (this.var is UInt32)
                return "uint32";
            else if (this.var is Int64)
                return "int64";
            else if (this.var is UInt64)
                return "uint64";
            else
                return "unknown";
        }


        private object createVariableFromString(string type, string value)
        {
            object newVar;

            switch (type.ToLower())
            {
                case "bool":
                    newVar = Boolean.Parse(value);
                    break;
                case "string":
                    newVar = new String(value.ToCharArray());
                    break;
                case "float":
                    newVar = float.Parse(value);
                    break;
                case "double":
                    newVar = double.Parse(value);
                    break;
                case "int16":
                    newVar = Int16.Parse(value);
                    break;
                case "uint16":
                    newVar = UInt16.Parse(value);
                    break;
                case "int32":
                    newVar = Int32.Parse(value);
                    break;
                case "uint32":
                    newVar = UInt32.Parse(value);
                    break;
                case "int64":
                    newVar = Int64.Parse(value);
                    break;
                case "uint64":
                    newVar = UInt64.Parse(value);
                    break;
                default:
                    newVar = new object();
                    break;
            }
            
            return newVar;
        }

        public string ToFullString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.getVariableTypeString() + " ");
            sb.Append(this.name + " ");
            sb.Append((this.getVariableTypeString().Equals("string") ? "\"" : "") + this.getVariableValueString() + (this.getVariableTypeString().Equals("string") ? "\"" : "") + " ");
            sb.Append(this.displayname + " ");

            return sb.ToString();
        }

        public override string ToString()
        {
            if (!String.IsNullOrWhiteSpace(this.displayname))
                return this.displayname;
            else
                return this.name;
        }
    }
}
