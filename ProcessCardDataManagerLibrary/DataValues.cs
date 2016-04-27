using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary
{
    public class DataValues 
    {
        public string Variable { get; set; }
        public DataType Type { get; set; }
        private object value = null;
        public object Value {
            get
            {
                return value;
            }
            set
            {
                if (value.GetType() == this.Type.CurrentType)
                {
                    this.value = value;
                }
                else
                {
                    throw new Exceptions.TypeMismatchException(this.Type.GetType(), value.GetType());
                }
            }
        }

        public DataValues(string Variable, DataType Type, object Value)
        {
            this.Variable = Variable;
            this.Type = Type;
            this.Value = Value;
        }

        public DataValues(string Varible, DataType Type)
        {
            this.Variable = Variable;
            this.Type = Type;
            
        }
    }
}
