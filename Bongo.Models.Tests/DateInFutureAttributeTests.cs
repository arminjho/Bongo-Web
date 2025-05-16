using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bongo.Models.ModelValidations;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Bongo.Models.Tests
{
    [TestFixture]
    public class DateInFutureAttributeTests
    {
        [TestCase(100,ExpectedResult =true)]
        [TestCase(-100,ExpectedResult =false)]
        [TestCase(0,ExpectedResult =false)]
        public bool DateValidator_InputExceptionDateRange_DateValidity(int addTime)
        {
            DateInFutureAttribute dateInFutureAttribute = new (()=>DateTime.Now);
            
            return dateInFutureAttribute.IsValid(DateTime.Now.AddSeconds(addTime));

        }

        [Test]
        public void DateValidator_NotValidDate_ReturnErrorMessage()
        {
            var result = new DateInFutureAttribute();   
            ClassicAssert.AreEqual("Date must be in the future", result.ErrorMessage); 
        }

      

    }
}
