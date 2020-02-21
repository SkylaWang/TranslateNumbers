using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TranslateNumbers.Exceptions;

namespace TranslateNumbers
{
    //A static class contain the translation functions.
    public static class Translation
    {
        //The entry of the translation function
        public static string TransalteCurrencyAmountToWords(string source)
        {
            //step 1: validation input
            IsValid(source);

            //step 2: for valid input, start translate
            //step 2.1: seprate a number as whole number part and decimal part, and translate each part respectively
            string[] wholeNumberAndDecimal = source.Trim().Split('.');

            long wholeNum = Int64.Parse(wholeNumberAndDecimal[0]);
            StringBuilder words = new StringBuilder();

            //step 2.2: translate whole numer part 
            if (wholeNum > 0)
            {
                words.Append(TranslatePositiveInt(wholeNum));
                //check if need plural noun 
                words.Append((wholeNum > 1 ? " Dollars" : " Dollar"));
            }

            //step 2.3: translate decimal part if have any
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

            //step 2.4: return result
            return words.ToString();
        }

        //Validation method
        private static void IsValid(string source)
        {
            //pattern validation. 
            //Assum that the valid input should be a positive number with maximun 2 decimal places.
            string patternReg = @"(^\d+(\.\d{1,2})?$)";
            Regex patternCheck = new Regex(patternReg);

            if (!patternCheck.IsMatch(source))
            {
                throw new InvalidPatternException();
            }

            //range validation.
            //Assum that the valid input should between 0.01 and 999999999999.99
            double sourceToNumber = Double.Parse(source);
            if(sourceToNumber < Constants.MIN || sourceToNumber > Constants.MAX)
            {
                throw new InvalidRangeException();
            }
             
        }

        //Core translate function
        //param number with long type because it should cover range (0,1000000000000)
        //this function can call itself to combine all part of translation into final result.
        private static string TranslatePositiveInt(long number)
        {
            //using string builder to build the final result
            StringBuilder word = new StringBuilder();

            long remainder, times;
            
            if (number >= Constants.BILLION)
            {
                remainder = number % Constants.BILLION;
                times = number / Constants.BILLION;

                //translate numbers before 'billion' in the result
                word.Append(TranslatePositiveInt(times));
                word.Append(" billion ");
                //translate numbers after 'billion' in the result
                word.Append(TranslatePositiveInt(remainder));
            }
            else if (number >= Constants.MILLION)
            {
                remainder = number % Constants.MILLION;
                times = number / Constants.MILLION;

                //translate numbers before 'million' in the result
                word.Append(TranslatePositiveInt(times));
                word.Append(" million ");
                //translate numbers before 'million' in the result
                word.Append(TranslatePositiveInt(remainder));
            }
            else if (number >= Constants.THOUSAND)
            {
                remainder = number % Constants.THOUSAND;
                times = number / Constants.THOUSAND;

                //translate numbers before 'thousand' in the result
                word.Append(TranslatePositiveInt(times));
                word.Append(" thousand ");
                //translate numbers before 'thousand' in the result
                word.Append(TranslatePositiveInt(remainder));
            }
            else if (number >= Constants.HUNDRED)
            {
                remainder = number % Constants.HUNDRED;
                times = number / Constants.HUNDRED;

                //translate numbers before 'hundred' in the result
                word.Append(Constants.BELOW_TWENTY[times - 1]);
                word.Append(" hundred ");
                //translate numbers before 'hundred' in the result
                word.Append(TranslatePositiveInt(remainder));

            }
            //for number that is equal or more than 20
            else if (number >= Constants.TWENTY)
            {
                remainder = number % Constants.TEN;
                times = number / Constants.TEN;
                //translate numbers times of 10
                word.Append(Constants.MORE_THAN_TWENTY[times - 2]);
                //translate numbers not times of ten
                word.Append((remainder > 0 ? "-" + TranslatePositiveInt(remainder) : string.Empty));
            }
            else if(number >= 1)
            {
                //translate numbers smaller than 20
                word.Append(Constants.BELOW_TWENTY[number - 1]);
            }
            
            return word.ToString().Trim();
        }
    }
}
