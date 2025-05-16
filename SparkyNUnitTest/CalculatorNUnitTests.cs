using NUnit.Framework;
using NUnit.Framework.Legacy;
using Sparky;

namespace SparkyNUnitTest
{
    [TestFixture]
    public class CalculatorNUnitTests
    {
        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange
            Calculator calc = new();

            //Act
            int result = calc.AddNumbers(10, 20);


            //Assert
            ClassicAssert.AreEqual(30, result);
        }

        [Test]
        public void IsOddChecker_InputEvenNumber_ReturnFalse()
        {
            Calculator calc = new();

            bool isOdd = calc.IsOddNumber(10);
            Assert.That(isOdd, Is.EqualTo(false));   
        }


        [Test]
        [TestCase(10, ExpectedResult =false)]
        [TestCase(11, ExpectedResult =true)]
        public bool IsOddChecker_InputEvenNumber_ReturnTrueIfOdd(int a)
        {
            Calculator calc = new();

            return calc.IsOddNumber(a); 
        }
    }
}
