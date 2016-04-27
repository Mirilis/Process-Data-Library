using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary.Exceptions
{
    public class TypeMismatchException : ArgumentException
    {
        public TypeMismatchException(Type A, Type B)
        {
            throw new ArgumentException("Type provided by Key: " + A.ToString() + " does not match type of Value: " + B.ToString());
        }
    }
}
