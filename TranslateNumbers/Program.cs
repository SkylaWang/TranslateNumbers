using System;
using TranslateNumbers.Exceptions;

namespace TranslateNumbers
{
    class Program
    {
        
        static void Main(string[] args)
        {
            bool isContinue = true;
            do
            {
                try
                {
                    Console.Write(Constants.INPUT);
                    string entry = Console.ReadLine();
                    //When enter "exit", it will end the loop
                    if (entry.ToUpper().Equals(Constants.EXIT))
                    {
                        isContinue = false;
                    }
                    else
                    {
                        //translate input number to words, including input validation and translation functions.
                        string result = Translation.TransalteCurrencyAmountToWords(entry.Trim());
                        Console.WriteLine(Constants.OUTPUT + result);
                    }
                }
                //error handling, can add future to log all exceptions
                catch (InvalidPatternException ex){
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch
                {
                    Console.WriteLine(Constants.OTHER_ERROR);
                }
            }
            while (isContinue);
        }
    }
}
