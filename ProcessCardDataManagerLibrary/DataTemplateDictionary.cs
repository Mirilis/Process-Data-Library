using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary
{
    public class DataTemplateDictionary : Dictionary<string,DataType>
    {
        public DataTemplateDictionary() : base()
        {
            
        }

        public DataTemplateDictionary(Dictionary<string,DataType> DataTypeDictionary) : base()
        {
            this.Clear();
            foreach (var variable in DataTypeDictionary)
            {
                this.Add(variable.Key, variable.Value);
            }
        }
    }
}
