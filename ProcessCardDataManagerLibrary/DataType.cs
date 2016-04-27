using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ProcessCardDataManagerLibrary
{
    public class DataType
    {
        public Type CurrentType { get; private set; }
        public object Blank { get; private set; }
        

        public DataType(string Type)
        {
            switch (Type.ToLower())
            {
                case "string":
                case "system.string":
                    this.CurrentType = typeof(string);
                    this.Blank = "Empty";
                    break;
                case "int":
                case "system.int32":
                    this.CurrentType = typeof(int);
                    this.Blank = 0;
                    break;
                case "double":
                case "system.double":
                    this.CurrentType = typeof(double);
                    this.Blank = 0.0;
                    break;
                case "system.path":
                    this.CurrentType = typeof(Path);
                    this.Blank = "No Path Specified";
                    break;
                default:
                    throw new ArgumentException("Type provided is not a valid data type for storage.");
            }
        }

        public static T GetInstance<T>(string type)
        {
            return (T)Activator.CreateInstance(Type.GetType(type));
        }



    }
}
