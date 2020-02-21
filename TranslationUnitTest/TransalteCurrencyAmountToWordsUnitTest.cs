using NUnit.Framework;
using TranslateNumbers;

namespace TranslationUnitTest
{
    public class TransalteCurrencyAmountToWordsUnitTest
    {       
        const double MIN = 0.01;
        const double MAX = 999999999999.99;

        const string ERROR_PATTERN = "Invalid number. It must be a positive number, up to 2 decimals.";
        string ERROR_RANGE = $"Invalid number. It must be between {MIN} and {MAX}";

        [SetUp]
        public void Setup()
        {            
        }

        [Test]
        public void TestInvalidPatternEntry()
        {
            string expectResult = ERROR_PATTERN;
            string actualResult = Translation.TransalteCurrencyAmountToWords("a4bc-890");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestOutOfMinEntry()
        {
            string expectResult = ERROR_RANGE;
            string actualResult = Translation.TransalteCurrencyAmountToWords("0");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestMinNumberEntry()
        {
            string expectResult = "one Cent";
            string actualResult = Translation.TransalteCurrencyAmountToWords(MIN.ToString());
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestOutofMaxEntry()
        {
            string expectResult = ERROR_RANGE;
            string actualResult = Translation.TransalteCurrencyAmountToWords("1000000000000");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestMaxNumberEntry()
        {
            string expectResult = "nine hundred ninety-nine billion nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine Dollars and ninety-nine Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords(MAX.ToString());
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestBelowOneWithOneDecimalEntry()
        {
            string expectResult = "ten Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords("0.1");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestOneDollarEntry()
        {
            string expectResult = "one Dollar";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestBetweenOneAndTwentyEntry()
        {
            string expectResult = "eleven Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("11");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestTwentyEntry()
        {
            string expectResult = "twenty Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("20");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestTwentyOneEntry()
        {
            string expectResult = "twenty-one Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("21");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestBetweenOneHundredAndOneThousandEntry()
        {
            string expectResult = "one hundred twenty-three Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("123");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestBetweenOneThousandAndOneMillionEntry()
        {
            string expectResult = "one thousand two hundred thirty-four Dollars and fifty-six Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1234.56");
            Assert.True(expectResult.Equals(actualResult));
        }


        [Test]
        public void TestBetweenOneMillionAndOneBillionEntry()
        {
            string expectResult = "one million two hundred thirty-four thousand five hundred sixty-seven Dollars and eighty-nine Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1234567.89");
            Assert.True(expectResult.Equals(actualResult));
        }

        [Test]
        public void TestMoreThanOneBillionEntry()
        {
            string expectResult = "one billion two hundred thirty-four million two hundred thirty-four thousand five hundred sixty-seven Dollars and eighty-nine Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1234234567.89");
            Assert.True(expectResult.Equals(actualResult));
        }
    }
}