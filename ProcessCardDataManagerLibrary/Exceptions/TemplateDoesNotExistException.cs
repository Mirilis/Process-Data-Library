using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary.Exceptions
{
    public class ObjectDoesNotExistException : Exception
    {
        public ObjectDoesNotExistException(string message) : base(message + " does not exist in Database.  Create Template.")
        {
        }
    }
}
