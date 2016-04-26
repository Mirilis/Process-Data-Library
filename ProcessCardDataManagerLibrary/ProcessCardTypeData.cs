using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary
{
    public class ProcessCardTypeData<Tkey,TValue> : Dictionary<string,ProcessCardDataType>
    {
        public ProcessCardTypeData():base()
        {
        }

        public new void Add(string Key, ProcessCardDataType Value)
        {
            if (this.ContainsKey(Key))
            {
                throw new ArgumentException("Key Already Exists in Collection.");
            }
        }

    }
}
