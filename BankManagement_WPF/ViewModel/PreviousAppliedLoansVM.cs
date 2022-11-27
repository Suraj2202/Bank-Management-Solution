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
using System.Windows.Controls;

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

        private DelegateCommand<BindableCollection<LoanDetail>> cancelLoanCommand;

        public DelegateCommand<BindableCollection<LoanDetail>> CancelLoanCommand => cancelLoanCommand ?? (cancelLoanCommand = new DelegateCommand<BindableCollection<LoanDetail>>(ExecutableCancelCommand));

        private void ExecutableCancelCommand(BindableCollection<LoanDetail> obj)
        {
            int check = GlobalVariables.LOANID;
            CancelLoanWindow window = new CancelLoanWindow();
            window.ShowDialog();
        }

        /*void ExecutableCancelCommand(BindableCollection<LoanDetail> obj)
        {
            //GlobalVariables.LOANID = obj;
            CancelLoanWindow window = new CancelLoanWindow();
            window.ShowDialog();
        }
*/
        TextBlockValidation textBlockValidation;

        public PreviousAppliedLoansCommand PreviousAppliedLoansCommand { get; set; }

        public PreviousAppliedLoansVM()
        {
            GlobalVariables.LOANID = 0;
            textBlockValidation = new TextBlockValidation();
            DisplayAllAttributes();
        }

        private async void DisplayAllAttributes()
        {
            var response = await PreviousAppliedLoansHelper.GetUserDetail(GlobalVariables.USERNAME);
            LoanDetails = new BindableCollection<LoanDetail>(response);
        }

        public event SelectionChangedEventHandler SelectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
