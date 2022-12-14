using BankManagement_WPF.Model;
using BankManagement_WPF.ViewModel;
using BankManagement_WPF.ViewModel.Commands;
using BMSAPI.Tests;
using Caliburn.Micro;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Tests
{
    [TestFixture]
    class AdminDashboardVM_Tests
    {
        private AdminDashboardVM adminDashboardVM;
        private ApprovedStatusCommand approveCommand;
        private RejectCommand rejectCommand;

        [SetUp]
        public void Setup()
        {
            GlobalVariables.USERNAME = "admin";
            GlobalVariables.COMMENT = "Approved";
            adminDashboardVM = new AdminDashboardVM();
        }

        [Test]
        public void BindableLoanDetails_Test()
        {
            List<LoanDetail> loanList = new List<LoanDetail>();
            loanList.Add(TestData.LoanDetail);
            adminDashboardVM.LoanDetails = new BindableCollection<LoanDetail>(loanList);

            Assert.IsNotNull(adminDashboardVM.LoanDetails);
        }

        [Test]
        public void ApproveCommand_Test()
        {
            approveCommand = new ApprovedStatusCommand(adminDashboardVM);
            List<LoanDetail> loanList = new List<LoanDetail>();
            loanList.Add(TestData.LoanDetail);
            adminDashboardVM.LoanDetails = new BindableCollection<LoanDetail>(loanList);

            GlobalVariables.LOANID = TestData.LoanDetail.LoanId;
            approveCommand.CanExecute(null);
            approveCommand.Execute(null);

            Assert.IsNotNull(adminDashboardVM.LoanDetails);
        }

        [Test]
        public void RejectCommand_Test()
        {
            rejectCommand = new RejectCommand(adminDashboardVM);
            List<LoanDetail> loanList = new List<LoanDetail>();
            loanList.Add(TestData.LoanDetail);
            adminDashboardVM.LoanDetails = new BindableCollection<LoanDetail>(loanList);

            GlobalVariables.LOANID = TestData.LoanDetail.LoanId;
            rejectCommand.CanExecute(null);
            rejectCommand.Execute(null);

            Assert.IsNotNull(adminDashboardVM.LoanDetails);
        }

        [Test]
        public void Search_Test()
        {
            List<LoanDetail> loanList = new List<LoanDetail>();
            loanList.Add(TestData.LoanDetail);
            adminDashboardVM.originalList = loanList;
            adminDashboardVM.LoanDetails = new BindableCollection<LoanDetail>(adminDashboardVM.originalList);
            adminDashboardVM.Search = "P";
            Assert.AreEqual(adminDashboardVM.originalList.Count(),adminDashboardVM.LoanDetails.Count);
        }
    }
}
