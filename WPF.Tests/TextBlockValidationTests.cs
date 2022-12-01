using BankManagement_WPF.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Tests
{
    [TestFixture]
    class TextBlockValidationTests
    {
        private TextBlockValidation textBlockValidation;

        [SetUp]
        public void Setup()
        {
            textBlockValidation = new TextBlockValidation();
        }

        [Test]
        [TestCase("suraj")]
        public void UserNameValidation_Pass(string username)
        {
            bool res = textBlockValidation.UserNameValidation(username);

            Assert.AreEqual(false, res);
        }
    }
}
