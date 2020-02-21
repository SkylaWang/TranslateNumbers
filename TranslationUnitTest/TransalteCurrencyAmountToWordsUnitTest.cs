using NUnit.Framework;
using TranslateNumbers;

namespace TranslationUnitTest
{
    public class TransalteCurrencyAmountToWordsUnitTest
    {   
        [SetUp]
        public void Setup()
        {            
        }

        //scenario 1: test invalid input, especially partten invalid
        [Test]
        public void TestInvalidPatternEntry()
        {
            string expectResult = Constants.ERROR_PATTERN;
            string actualResult = Translation.TransalteCurrencyAmountToWords("a4bc-890");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 2: test invalid input, especially smaller than min value
        [Test]
        public void TestOutOfMinEntry()
        {
            string expectResult = Constants.ERROR_RANGE;
            string actualResult = Translation.TransalteCurrencyAmountToWords("0");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 3: test boundar input, especially min value
        [Test]
        public void TestMinNumberEntry()
        {
            string expectResult = "one Cent";
            string actualResult = Translation.TransalteCurrencyAmountToWords(Constants.MIN.ToString());
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 4: test invalid input, especially more than max value
        [Test]
        public void TestOutofMaxEntry()
        {
            string expectResult = Constants.ERROR_RANGE;
            string actualResult = Translation.TransalteCurrencyAmountToWords("1000000000000");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 5: test boundar input, especially max value
        [Test]
        public void TestMaxNumberEntry()
        {
            string expectResult = "nine hundred ninety-nine billion nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine Dollars and ninety-nine Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords(Constants.MAX.ToString());
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 6: test one value from (0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9)
        [Test]
        public void TestBelowOneWithOneDecimalEntry()
        {
            string expectResult = "ten Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords("0.1");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 7: test boundar input, number is 1
        [Test]
        public void TestOneDollarEntry()
        {
            string expectResult = "one Dollar";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 8: test number between 1 and 20, cover logic if(number > 1 && number < 20)
        [Test]
        public void TestBetweenOneAndTwentyEntry()
        {
            string expectResult = "eleven Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("11");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 9: test number between 20 and 100, cover logic if(number >= 20 && number < 100)
        //test boundar input, number is 20
        //especially for number is times of 10
        [Test]
        public void TestTwentyEntry()
        {
            string expectResult = "twenty Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("20");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 10: test number between 20 and 100, cover logic if(number >= 20 && number < 100)
        //especially for number is not times of 10
        [Test]
        public void TestTwentyOneEntry()
        {
            string expectResult = "twenty-one Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("21");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 11: test number between 100 and 1000, cover logic if(number >= 100 && number < 1000)
        [Test]
        public void TestBetweenOneHundredAndOneThousandEntry()
        {
            string expectResult = "one hundred twenty-three Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("123");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 12: test boundar input, number is 100
        [Test]
        public void TestOneHundredEntry()
        {
            string expectResult = "one hundred Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("100");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 13: test number between 1000 and 1 million, cover logic if(number >= 1000 && number < 1000000)
        [Test]
        public void TestBetweenOneThousandAndOneMillionEntry()
        {
            string expectResult = "one thousand two hundred thirty-four Dollars and fifty-six Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1234.56");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 14: test boundar input, number is 1000
        [Test]
        public void TestOneThousandEntry()
        {
            string expectResult = "one thousand Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1000");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 15: test number between 1 million and 1 billion, cover logic if(number >= 1000000 && number < 1000000000)
        [Test]
        public void TestBetweenOneMillionAndOneBillionEntry()
        {
            string expectResult = "one million two hundred thirty-four thousand five hundred sixty-seven Dollars and eighty-nine Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1234567.89");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 16: test boundar input, number is 1 million
        [Test]
        public void TestOneMillionEntry()
        {
            string expectResult = "one million Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1000000");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 17: test number more than 1 billion, cover logic if(number >= 1000000000)
        [Test]
        public void TestMoreThanOneBillionEntry()
        {
            string expectResult = "one billion two hundred thirty-four million two hundred thirty-four thousand five hundred sixty-seven Dollars and eighty-nine Cents";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1234234567.89");
            Assert.True(expectResult.Equals(actualResult));
        }

        //scenario 18: test boundar input, number is 1 billion
        [Test]
        public void TestOneBillionEntry()
        {
            string expectResult = "one billion Dollars";
            string actualResult = Translation.TransalteCurrencyAmountToWords("1000000000");
            Assert.True(expectResult.Equals(actualResult));
        }
    }
}