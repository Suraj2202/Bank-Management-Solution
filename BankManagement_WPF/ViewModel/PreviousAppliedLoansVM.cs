using BankManagement_WPF.Model;
using BankManagement_WPF.Validations;
using BankManagement_WPF.View;
using BankManagement_WPF.ViewModel.Commands;
using BankManagement_WPF.ViewModel.Helpers;
using Caliburn.Micro;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel
{
    class PreviousAppliedLoansVM : INotifyPropertyChanged
    {

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

        private DelegateCommand<LoanDetail> cancelLoanCommand;

        public DelegateCommand<LoanDetail> CancelLoanCommand => cancelLoanCommand ?? (cancelLoanCommand = new DelegateCommand<LoanDetail>(ExecutableCancelCommand));

        void ExecutableCancelCommand(LoanDetail obj)
        {
            throw new NotImplementedException();
        }

        TextBlockValidation textBlockValidation;

        public PreviousAppliedLoansCommand PreviousAppliedLoansCommand { get; set; }

        public PreviousAppliedLoansVM()
        {
            textBlockValidation = new TextBlockValidation();
            DisplayAllAttributes();
        }

        private async void DisplayAllAttributes()
        {
            var response = await PreviousAppliedLoansHelper.GetUserDetail(GlobalVariables.USERNAME);
            LoanDetails = new BindableCollection<LoanDetail>(response);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
