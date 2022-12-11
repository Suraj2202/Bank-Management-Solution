using BankManagement_WPF.Model;
using BankManagement_WPF.ViewModel;
using BankManagement_WPF.ViewModel.Commands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Tests
{
    [TestFixture]
    class SignupVM_Tests
    {
        private SignupVM signupVM;
        private CreateAccountCommand createAccountCommand;
        private UserDetail userDetail = TestData.UserDetail;

        [SetUp]
        public void Setup()
        {
            signupVM = new SignupVM();
            createAccountCommand = new CreateAccountCommand(signupVM);

            signupVM.Name = userDetail.Name;
            signupVM.UserName = userDetail.UserName;
            signupVM.PassWord = userDetail.Password;
            signupVM.Address = userDetail.Address;
            signupVM.State = userDetail.State;
            signupVM.Country = userDetail.Country;
            signupVM.ContactNo = userDetail.Contact.ToString();
            signupVM.PAN = userDetail.PAN.ToString();
            signupVM.EmailId = userDetail.Email;
            signupVM.DOB = userDetail.DOB.ToString();
            signupVM.AccountType = "System.Windows.Controls.ComboBoxItem: Saving";
        }

        [Test]
        public void CreateAccount_Test()
        {
            createAccountCommand.CanExecute(null);
            createAccountCommand.Execute(null);

            Assert.IsNull(signupVM.Warning);
        }


        [Test]
        public void SignupVM_Dash_LoanDate_Test()
        {
            signupVM.DOB = "02-02-2020";
            signupVM.CreateNewAccount();

            Assert.IsNotNull(signupVM.DOB);
        }

        [Test]
        public void SignupVM_Shlash_LoanDate_Test()
        {
            signupVM.DOB = "02/02/2020";
            signupVM.CreateNewAccount();

            Assert.IsNotNull(signupVM.DOB);
        }

        [Test]
        public void SignupVM_Error_Test()
        {
            signupVM.UserName = "@#";
            signupVM.PassWord = "test";
            signupVM.EmailId = "wrong";
            signupVM.PAN = "098";
            signupVM.ContactNo = "989";
            signupVM.DOB = DateTime.Now.ToString("dd/MM/yyyy");
            signupVM.Warning = "";

            Assert.IsTrue(signupVM.PropertyErrors.Any());
        }


        [Test]
        public void SignupVM_UserName_Empty_Errors_Test()
        {
            signupVM.UserName = "";
            signupVM.CreateNewAccount();

            Assert.IsNotNull(signupVM.UserName);
        }

        [Test]
        public void SignupVM_UserName_Errors_Test()
        {
            signupVM.UserName = "Tes67%*";
            signupVM.CreateNewAccount();

            Assert.IsNotNull(signupVM.UserName);
        }

        [Test]
        public void SignupVM_PassWord_Errors_Test()
        {
            signupVM.PassWord = "T67%*";
            signupVM.CreateNewAccount();

            Assert.IsNotNull(signupVM.PassWord);
        }


        [Test]
        public void SignupVM_EmailId_Errors_Test()
        {
            signupVM.EmailId = "T67%*";
            signupVM.CreateNewAccount();

            Assert.IsNotNull(signupVM.EmailId);
        }

        [Test]
        public void SignupVM_PAN_Errors_Test()
        {
            signupVM.PAN = "098*";
            signupVM.CreateNewAccount();

            Assert.IsNotNull(signupVM.PAN);
        }

        [Test]
        public void SignupVM_Contact_Errors_Test()
        {
            signupVM.ContactNo = "098*";
            signupVM.CreateNewAccount();

            Assert.IsNotNull(signupVM.ContactNo);
        }

        [Test]
        public void SignupVM_Age_Errors_Test()
        {
            signupVM.DOB = DateTime.Now.ToString("dd/MM/yyyy"); ;
            signupVM.CreateNewAccount();

            Assert.IsNotNull(signupVM.DOB);
        }

    }
}
