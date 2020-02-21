using System;
using System.Collections.Generic;
using System.Text;

namespace TranslateNumbers.Exceptions
{
    public class InvalidPatternException:Exception
    {
        public InvalidPatternException() : base(Constants.ERROR_PATTERN) { }
    }
}
