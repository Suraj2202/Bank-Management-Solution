using BankManagement_WPF.View;
using BankManagement_WPF.ViewModel.Commands;
using BankManagement_WPF.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankManagement_WPF.ViewModel
{
    public class CancelLoanVM
    {
        public CancelLoanCommand CancelLoanCommand { get; set; }

        public CancelLoanVM()
        {
            CancelLoanCommand = new CancelLoanCommand(this);
        }

        public async void CancelLoanStatus()
        {
            await UpdateDetailHelper.UpdateLoanStatus(GlobalVariables.LOANID, "Cancel");
            
        }
    }
}
