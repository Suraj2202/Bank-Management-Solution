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
        [TestCase("123456")]
        public void UserNameValidation_Pass_Test(string username)
        {
            bool res = textBlockValidation.UserNameValidation(username);

            Assert.IsFalse(res);
        }

        [Test]
        [TestCase("suraj%")]
        [TestCase("!@#$%^*")]
        public void UserNameValidation_Fail_Test(string username)
        {
            bool res = textBlockValidation.UserNameValidation(username);

            Assert.IsTrue(res);
        }

        [Test]
        [TestCase("Suraj@12")]
        [TestCase("Suraj&007")]
        public void PasswordValidation_Pass_Test(string password)
        {
            bool res = textBlockValidation.PasswordValidation(password);

            Assert.IsFalse(res);
        }

        [Test]
        [TestCase("Suraj12")]
        [TestCase("suraj#12")]
        [TestCase("12")]
        [TestCase("&%^&!")]
        public void PasswordValidation_Fail_Test(string password)
        {
            bool res = textBlockValidation.PasswordValidation(password);

            Assert.IsTrue(res);
        }

        [Test]
        [TestCase("1234567890")]
        [TestCase("9999999999")]
        [TestCase("9876543210")]
        public void PANandContactNoValidation_Pass_Test(string pan)
        {
            bool res = textBlockValidation.PANandContactNoValidation(pan);

            Assert.IsFalse(res);
        }

        [Test]
        [TestCase("0000000000")]
        [TestCase("0123456789")]
        [TestCase("99999999")]
        public void PANandContactNoValidation_Fail_Test(string pan)
        {
            bool res = textBlockValidation.PANandContactNoValidation(pan);

            Assert.IsTrue(res);
        }

        [Test]
        [TestCase("suraj@gmail.com")]
        [TestCase("try@try.try")]
        public void EmailIDValidation_Pass_Test(string emailId)
        {
            bool res = textBlockValidation.EmailIDValidation(emailId);

            Assert.IsFalse(res);
        }

        [Test]
        [TestCase("suraj@q.com")]
        [TestCase("trytry.try")]
        [TestCase("trytrytry")]
        public void EmailIDValidation_Fail_Test(string emailId)
        {
            bool res = textBlockValidation.EmailIDValidation(emailId);

            Assert.IsTrue(res);
        }

        [Test]
        [TestCase("12/08/1997")]
        [TestCase("02/28/1990")]
        public void FutureDateValidation_Pass_Test(string futureDate)
        {
            bool res = textBlockValidation.FutureDateValidation(futureDate);

            Assert.IsFalse(res);
        }

        [Test]
        [TestCase("02/02/2029")]
        [TestCase("02/02/5029")]
        public void FutureDateValidation_Fail_Test(string futureDate)
        {
            bool res = textBlockValidation.FutureDateValidation(futureDate);

            Assert.IsTrue(res);
        }

        [Test]
        [TestCase("12/08/1997")]
        [TestCase("02/28/1990")]
        public void AgeGreaterThan18_Pass_Test(string date)
        {
            bool res = textBlockValidation.AgeGreaterThan18(date);

            Assert.IsFalse(res);
        }

        [Test]
        [TestCase("02/02/2029")]
        [TestCase("12/12/2022")]
        public void AgeGreaterThan18_Fail_Test(string date)
        {
            bool res = textBlockValidation.AgeGreaterThan18(date);

            Assert.IsTrue(res);
        }
    }
}
