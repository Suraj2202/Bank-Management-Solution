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
    class PreviousAppliedLoansVM_Tests
    {
        private PreviousAppliedLoansVM previousAppliedLoansVM;
        
        [SetUp]
        public void Setup()
        {
            previousAppliedLoansVM = new PreviousAppliedLoansVM();
        }
    }
}
