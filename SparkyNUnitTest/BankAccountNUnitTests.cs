using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Sparky;

namespace SparkyNUnitTest
{
    [TestFixture]
    public class BankAccountNUnitTests
    {
        private BankAccount bankAccount;
        [SetUp]
        public void Setup()
        {
            bankAccount=new BankAccount(new LogBook());  
        }
        [Test]  
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock=new Mock<ILogBook>();
            logMock.Setup(x => x.Message(""));
            
            BankAccount bankAccount=new BankAccount(logMock.Object);    

            var result = bankAccount.Deposit(100);

            ClassicAssert.IsTrue(result);  

            Assert.That(bankAccount.GetBalance(), Is.EqualTo(100)); 
        }

        [Test]
        public void BankLogDummy_LogMockString_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello";

            logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());

            Assert.That(logMock.Object.MessageWithReturnStr("HELLo"), Is.EqualTo(desiredOutput));
        }

    }
}
