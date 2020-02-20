using System;

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
                Console.Write("InPut: ");
                string entry = Console.ReadLine();
                if (entry.ToUpper().Equals(EXIT))
                {
                    isContinue = false;
                }
                else
                {
                    string result = Translation.DoTransalte(entry);
                    Console.WriteLine("OutPut: " + result);
                }
            }
            while (isContinue);
        }
    }
}
