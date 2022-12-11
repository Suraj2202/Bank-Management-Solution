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
    class CancelLoanVM_Test
    {
        private CancelLoanVM cancelLoanVM;
        private CancelLoanCommand cancelLoanCommand;

        [SetUp]
        public void Setup()
        {
            cancelLoanVM = new CancelLoanVM();
            cancelLoanCommand = new CancelLoanCommand(cancelLoanVM);
            GlobalVariables.COMMENT = "Cancel";
        }

        [Test]
        public void CancelLoanStatus_Test()
        {
            cancelLoanCommand.CanExecute(null);
            cancelLoanCommand.Execute(null);
            Assert.IsNotNull(GlobalVariables.COMMENT);
        }

    }
}
