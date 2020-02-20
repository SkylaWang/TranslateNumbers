using System;
using System.Collections.Generic;
using System.Text;

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

        public static string DoTransalte(string source)
        {
            ValidationResult validation = IsValid(source);
            if (validation.IsValid)
            {
                string[] wholeNumberAndDecimal = source.Trim().Split('.');
                
                int wholeNum = Int32.Parse(wholeNumberAndDecimal[0]);
                string words = string.Empty;
                if (wholeNum > 0)
                {
                    words = TranslatePositiveInt(wholeNum) + (wholeNum > 1 ? " dollars" : " dollar");
                }
                
                if (wholeNumberAndDecimal.Length > 1)
                {
                    int decimalNum = Int32.Parse(wholeNumberAndDecimal[1]);
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
            return new ValidationResult
            {
                IsValid = true
            };
        }
        private static string TranslatePositiveInt(int number, List<string> currentWords = null)
        {
            string word = string.Empty;
            List<string> words = currentWords?? new List<string>();

            int below, times;

            if (number > BILLION)
            {
                below = number % BILLION;
                times = number / BILLION;
                word = TranslatePositiveInt(times) + " billion " + TranslatePositiveInt(below);
            }
            else if (number > MILLION)
            {
                below = number % MILLION;
                times = number / MILLION;
                word = TranslatePositiveInt(times) + " million " + TranslatePositiveInt(below);
            }
            else if (number > THOUSAND)
            {
                below = number % THOUSAND;
                times = number / THOUSAND;
                word = TranslatePositiveInt(times) + " thousand " + TranslatePositiveInt(below);
            }
            else if (number > HUNDRED)
            {
                below = number % HUNDRED;
                times = number / HUNDRED;
                word = BELOW_TWENTY[times - 1] + " hundred " + TranslatePositiveInt(below);

            }
            else if(number >= TWENTY)
            {
                below = number % TEN;
                times = number / TEN;
                word = MORE_THAN_TWENTY[times - 2] + (below > 0 ? "-" + TranslatePositiveInt(below) : string.Empty);
            }
            else if(number >= 1)
            {
                word = BELOW_TWENTY[number - 1];
            }
            
            return word;
        }
    }
}
