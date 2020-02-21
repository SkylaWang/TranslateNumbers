using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TranslateNumbers
{
    //A static class contain the translation functions.
    public static class Translation
    {
        //private constance fields
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

        //The entry of the translation function
        public static string TransalteCurrencyAmountToWords(string source)
        {
            //step 1: validation input
            ValidationResult validation = IsValid(source);
            
            //step 2.1: for valid input, start translate
            if (validation.IsValid)
            {
                //step 3: seprate a number as whole number part and decimal part, and translate each part respectively
                string[] wholeNumberAndDecimal = source.Trim().Split('.');
                
                long wholeNum = Int64.Parse(wholeNumberAndDecimal[0]);
                StringBuilder words = new StringBuilder();

                //step 4.1: translate whole numer part 
                if (wholeNum > 0)
                {
                    words.Append(TranslatePositiveInt(wholeNum));
                    //check if need plural noun 
                    words.Append((wholeNum > 1 ? " Dollars" : " Dollar"));
                }
                
                //step 4.2: translate decimal part if have any
                if (wholeNumberAndDecimal.Length > 1)
                {
                    long decimalNum = Int64.Parse(wholeNumberAndDecimal[1]);
                    if (decimalNum > 0)
                    {
                        //if there is one decimal place, should regard the value as 10 times, e.g. 0.1 = 0.10
                        decimalNum = wholeNumberAndDecimal[1].Length == 1 ? decimalNum * 10 : decimalNum;
                        words.Append(words.ToString().Equals(string.Empty) ? string.Empty : " and ");
                        //check if need plural noun 
                        words.Append(TranslatePositiveInt(decimalNum) + (decimalNum > 1 ? " Cents" : " Cent"));
                    }
                }

                return words.ToString();
            }
            //step 2.2: for invalid input, reply error message
            else
            {
                return validation.ErrorMsg;
            }

        }

        //Validation method
        private static ValidationResult IsValid(string source)
        {
            //pattern validation. 
            //Assum that the valid input should be a positive number with maximun 2 decimal places.
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

            //range validation.
            //Assum that the valid input should between 0.01 and 999999999999.99
            double sourceToNumber = Double.Parse(source);
            if(sourceToNumber < MIN || sourceToNumber > MAX)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMsg = ERROR_RANGE
                };
            }
             
            //when pass all validation, return true as IsValid
            return new ValidationResult
            {
                IsValid = true
            };
        }

        //Core translate function
        //param number with long type because it should cover range (0,1000000000000)
        //this function can call itself to combine all part of translation into final result.
        private static string TranslatePositiveInt(long number)
        {
            //using string builder to build the final result
            StringBuilder word = new StringBuilder();

            long remainder, times;
            
            if (number >= BILLION)
            {
                remainder = number % BILLION;
                times = number / BILLION;

                //translate numbers before 'billion' in the result
                word.Append(TranslatePositiveInt(times));
                word.Append(" billion ");
                //translate numbers after 'billion' in the result
                word.Append(TranslatePositiveInt(remainder));
            }
            else if (number >= MILLION)
            {
                remainder = number % MILLION;
                times = number / MILLION;

                //translate numbers before 'million' in the result
                word.Append(TranslatePositiveInt(times));
                word.Append(" million ");
                //translate numbers before 'million' in the result
                word.Append(TranslatePositiveInt(remainder));
            }
            else if (number >= THOUSAND)
            {
                remainder = number % THOUSAND;
                times = number / THOUSAND;

                //translate numbers before 'thousand' in the result
                word.Append(TranslatePositiveInt(times));
                word.Append(" thousand ");
                //translate numbers before 'thousand' in the result
                word.Append(TranslatePositiveInt(remainder));
            }
            else if (number >= HUNDRED)
            {
                remainder = number % HUNDRED;
                times = number / HUNDRED;

                //translate numbers before 'hundred' in the result
                word.Append(BELOW_TWENTY[times - 1]);
                word.Append(" hundred ");
                //translate numbers before 'hundred' in the result
                word.Append(TranslatePositiveInt(remainder));

            }
            //for number that is equal or more than 20
            else if (number >= TWENTY)
            {
                remainder = number % TEN;
                times = number / TEN;
                //translate numbers times of 10
                word.Append(MORE_THAN_TWENTY[times - 2]);
                //translate numbers not times of ten
                word.Append((remainder > 0 ? "-" + TranslatePositiveInt(remainder) : string.Empty));
            }
            else if(number >= 1)
            {
                //translate numbers smaller than 20
                word.Append(BELOW_TWENTY[number - 1]);
            }
            
            return word.ToString().Trim();
        }
    }
}
