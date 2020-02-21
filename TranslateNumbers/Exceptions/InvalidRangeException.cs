using System;
using System.Collections.Generic;
using System.Text;

namespace TranslateNumbers.Exceptions
{
    public class InvalidRangeException : Exception
    {
        public InvalidRangeException() : base(Constants.ERROR_RANGE) { }
    }
}
