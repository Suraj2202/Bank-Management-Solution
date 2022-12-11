using BankManagement_WPF.ViewModel;
using BankManagement_WPF.ViewModel.Commands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Tests
{
    [TestFixture]
    class LoginSecurityVM_Tests
    {
        private LoginSecurityVM loginSecurityVM;
        private LoginSecurityCommand loginSecurityCommand;
        private SignupCommand signupCommand;

        [SetUp]
        public void Setup()
        {
            loginSecurityVM = new LoginSecurityVM();
            loginSecurityCommand = new LoginSecurityCommand(loginSecurityVM);
            signupCommand = new SignupCommand(loginSecurityVM);
        }

        [Test]
        public void SignupWindow_Test()
        {
            // create a thread  
            Thread newWindowThread = new Thread(new ThreadStart(() =>
            {
                // create and show the window
                // your code
                signupCommand.CanExecute(null);
                signupCommand.Execute(null);

                // start the Dispatcher processing  
                System.Windows.Threading.Dispatcher.Run();
            }));

            // set the apartment state  
            newWindowThread.SetApartmentState(ApartmentState.STA);

            // make the thread a background thread  
            newWindowThread.IsBackground = true;

            // start the thread  
            newWindowThread.Start();
            

            Assert.IsNull(loginSecurityVM.UserName);
        }

        [Test]
        [TestCase("suraj")]
        [TestCase("123456")]
        public void LoginSecurityVM_UserNameValidation_Pass_Test(string username)
        {
            loginSecurityVM.UserName = username;
            Assert.AreEqual(username, loginSecurityVM.UserName);
        }

        [Test]
        [TestCase("suraj%")]
        [TestCase("!@#$%^*")]
        public void LoginSecurityVM_UserNameValidation_Fail_Test(string username)
        {
            loginSecurityVM.UserName = username;
            Assert.AreEqual(username, loginSecurityVM.UserName);
        }

        [Test]
        [TestCase("Suraj@12")]
        [TestCase("Suraj&007")]
        public void LoginSecurityVM_PasswordValidation_Pass_Test(string password)
        {
            loginSecurityVM.PassWord = password;
            Assert.AreEqual(password, loginSecurityVM.PassWord);
        }

        [Test]
        [TestCase("Suraj12")]
        [TestCase("suraj#12")]
        [TestCase("12")]
        [TestCase("&%^&!")]
        public void LoginSecurityVM_PasswordValidation_Fail_Test(string password)
        {
            loginSecurityVM.PassWord = password;
            Assert.AreEqual(password, loginSecurityVM.PassWord);
        }

        [Test]
        public void LoginSecurityQuery_Test()
        {
            loginSecurityVM.UserName = "test";
            loginSecurityVM.PassWord = "Test@121";

            loginSecurityCommand.CanExecute(null);
            loginSecurityCommand.Execute(null);

            Assert.IsNull(loginSecurityVM.Warning);

        }

        [Test]
        public void LoginSecurityQuery_UserName_Fail_Test()
        {
            loginSecurityVM.PassWord = "Test@121";

            loginSecurityCommand.CanExecute(null);
            loginSecurityCommand.Execute(null);

            Assert.IsNull(loginSecurityVM.UserName);

        }

        [Test]
        public void LoginSecurityQuery_Password_Fail_Test()
        {
            loginSecurityVM.UserName = "test";

            loginSecurityCommand.CanExecute(null);
            loginSecurityCommand.Execute(null);

            Assert.IsNull(loginSecurityVM.PassWord);

        }

        [Test]
        public void LoginSecurityVM_Warning_Test()
        {
            loginSecurityVM.Warning = "Something Went Wrong";
            Assert.NotNull(loginSecurityVM.Warning);
        }

        [Test]
        public void LoginSecurityVM_PropertError_Test()
        {
            loginSecurityVM.PropertyErrors.Add("test", "test");
            Assert.NotNull(loginSecurityVM.PropertyErrors);
        }


    }
}
