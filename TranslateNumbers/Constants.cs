using System;
using System.Collections.Generic;
using System.Text;

namespace TranslateNumbers
{
    public static class Constants
    {
        public static readonly string[] BELOW_TWENTY = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
                                                  "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen",
                                                  "eighteen", "nineteen" };
        public static readonly string[] MORE_THAN_TWENTY = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        public const int TEN = 10;
        public const int TWENTY = 20;
        public const int HUNDRED = 100;
        public const int THOUSAND = 1000;
        public const int MILLION = 1000000;
        public const int BILLION = 1000000000;

        public const double MIN = 0.01;
        public const double MAX = 999999999999.99;

        public const string ERROR_PATTERN = "Error: Invalid number. It must be a positive number, up to 2 decimals.";
        public static string ERROR_RANGE = $"Error: Invalid number. It must be between {MIN} and {MAX}";
        public const string OTHER_ERROR = "Error: Some unexpected error happens. Please try again.";

        public const string EXIT = "EXIT";
        public const string INPUT = "InPut: ";
        public const string OUTPUT = "OutPut: ";
    }
}
