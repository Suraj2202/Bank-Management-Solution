using BankManagement_WPF.Model;
using BankManagement_WPF.ViewModel.Helpers;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel
{
    class AdminDashboardVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private BindableCollection<LoanDetail> loanDetails;

        public BindableCollection<LoanDetail> LoanDetails
        {
            get { return loanDetails; }
            set
            {
                loanDetails = value;
                OnPropertyChanged("LoanDetails");
            }
        }

        public AdminDashboardVM()
        {
            DisplayAllAttributes();
        }

        private async void DisplayAllAttributes()
        {
            var response = await PreviousAppliedLoansHelper.GetAdminLoanDetail();
            LoanDetails = new BindableCollection<LoanDetail>(response);
        }

        public async void ApproveCommand()
        {
            await UpdateDetailHelper.UpdateLoanStatus(GlobalVariables.LOANID, "APPROVED");
            DisplayAllAttributes();
        }

        public async void RejectCommand()
        {
            await UpdateDetailHelper.UpdateLoanStatus(GlobalVariables.LOANID, "REJECTED");
            DisplayAllAttributes();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
