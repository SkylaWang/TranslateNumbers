using System;
using TranslateNumbers.Exceptions;

namespace TranslateNumbers
{
    class Program
    {
        const string EXIT = "EXIT";
        static void Main(string[] args)
        {
            bool isContinue = true;
            do
            {
                try
                {
                    Console.Write("InPut: ");
                    string entry = Console.ReadLine();
                    //When enter "exit", it will end the loop
                    if (entry.ToUpper().Equals(EXIT))
                    {
                        isContinue = false;
                    }
                    else
                    {
                        //translate input number to words, including input validation and translation functions.
                        string result = Translation.TransalteCurrencyAmountToWords(entry.Trim());
                        Console.WriteLine("OutPut: " + result);
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
