using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TranslateNumbers
{
    public static class Translation
    {
        static readonly string[] BELOW_TWENTY = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", 
                                                  "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", 
                                                  "eighteen", "nineteen" };
        static readonly string[] MORE_THAN_TWENTY = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};

        const int TEN = 10;
        const int TWENTY = 20;
        const int HUNDRED = 100;
        const int THOUSAND = 1000;
        const int MILLION = 1000000;
        const int BILLION = 1000000000;

        const double MIN = 0.01;
        const double MAX = 999999999999.99;

        const string ERROR_PATTERN = "Invalid number. It must be a positive number, up to 2 decimals.";
        static string ERROR_RANGE = $"Invalid number. It must be between {MIN} and {MAX}";

        public static string TransalteCurrencyAmountToWords(string source)
        {
            ValidationResult validation = IsValid(source);
            if (validation.IsValid)
            {
                string[] wholeNumberAndDecimal = source.Trim().Split('.');
                
                long wholeNum = Int64.Parse(wholeNumberAndDecimal[0]);
                string words = string.Empty;
                if (wholeNum > 0)
                {
                    words = TranslatePositiveInt(wholeNum) + (wholeNum > 1 ? " Dollars" : " Dollar");
                }
                
                if (wholeNumberAndDecimal.Length > 1)
                {
                    long decimalNum = Int64.Parse(wholeNumberAndDecimal[1]);
                    if (decimalNum > 0)
                    {
                        decimalNum = wholeNumberAndDecimal[1].Length == 1 ? decimalNum * 10 : decimalNum;
                        words = (words.Equals(string.Empty) ? words : (words + " and "))
                                + TranslatePositiveInt(decimalNum) + (decimalNum > 1 ? " Cents" : " Cent");
                    }
                }

                return words;
            }
            else
            {
                return validation.ErrorMsg;
            }

        }

        private static ValidationResult IsValid(string source)
        {
            string patternReg = @"(^\d+(\.\d{1,2})?$)";
            Regex patternCheck = new Regex(patternReg);

            if (!patternCheck.IsMatch(source))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMsg = ERROR_PATTERN
                };
            }

            double sourceToNumber = Double.Parse(source);
            if(sourceToNumber < MIN || sourceToNumber > MAX)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMsg = ERROR_RANGE
                };
            }
                
            return new ValidationResult
            {
                IsValid = true
            };
        }
        private static string TranslatePositiveInt(long number)
        {
            StringBuilder word = new StringBuilder();

            long below, times;

            if (number > BILLION)
            {
                below = number % BILLION;
                times = number / BILLION;
                word.Append(TranslatePositiveInt(times));
                word.Append(" billion ");
                word.Append(TranslatePositiveInt(below));
            }
            else if (number > MILLION)
            {
                below = number % MILLION;
                times = number / MILLION;
                word.Append(TranslatePositiveInt(times));
                word.Append(" million ");
                word.Append(TranslatePositiveInt(below));
            }
            else if (number > THOUSAND)
            {
                below = number % THOUSAND;
                times = number / THOUSAND;
                word.Append(TranslatePositiveInt(times));
                word.Append(" thousand ");
                word.Append(TranslatePositiveInt(below));
            }
            else if (number > HUNDRED)
            {
                below = number % HUNDRED;
                times = number / HUNDRED;
                word.Append(BELOW_TWENTY[times - 1]);
                word.Append(" hundred ");
                word.Append(TranslatePositiveInt(below));

            }
            else if(number >= TWENTY)
            {
                below = number % TEN;
                times = number / TEN;
                word.Append(MORE_THAN_TWENTY[times - 2]);
                word.Append((below > 0 ? "-" + TranslatePositiveInt(below) : string.Empty));
            }
            else if(number >= 1)
            {
                word.Append(BELOW_TWENTY[number - 1]);
            }
            
            return word.ToString();
        }
    }
}
