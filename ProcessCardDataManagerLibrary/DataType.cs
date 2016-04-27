using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary
{
    public class DataType
    {
        public Type CurrentType { get; private set; }
        

        public DataType(string Type)
        {
            switch (Type.ToLower())
            {
                case "string":
                    this.CurrentType = typeof(string);
                    break;
                case "int":
                    this.CurrentType = typeof(int);
                    break;
                case "double":
                    this.CurrentType = typeof(double);
                    break;
                case "image":
                    this.CurrentType = typeof(Image);
                    break;
                default:
                    throw new ArgumentException("Type provided is not a valid data type for storage.");
            }
        }



    }
}
