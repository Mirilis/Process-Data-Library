using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary.Exceptions
{
    class ObjectExistsException : Exception
    {
        public ObjectExistsException(string message) : base(message + " already exists in Database.")
        {
        }
        
    }
}
