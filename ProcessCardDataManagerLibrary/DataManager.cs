using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary
{
    public class DataManager
    {
        public List<DataValues> Data
        {
            get
            {
                return null;
            }

        }

        public DataManager(string DocumentName)
        {
            using (var context = new ProcessDocumentDataContainer())
            {
                //foreach (var item in collection)
                //{
                    
                //}
            }
        }
    }
}
