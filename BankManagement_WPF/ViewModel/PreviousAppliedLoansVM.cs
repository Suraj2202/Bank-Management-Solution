using BankManagement_WPF.Model;
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

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged("UserName"); }
        }


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

        private async void ExecutableCancelCommand(BindableCollection<LoanDetail> obj)
        {
            string checkValue = LoanDetails[GlobalVariables.LOANID - 1].Status;
            if (checkValue != "Pending")
            {
                System.Windows.MessageBox.Show("Can't Change the Status");
                return;
            }
            IUpdateDetailHelper update = new UpdateDetailHelper();
            await update.UpdateLoanStatus(GlobalVariables.LOANID, "CANCELED");
            DisplayAllAttributes();
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
            UserName = GlobalVariables.USERNAME.ToUpperInvariant();
            PreviousAppliedLoansHelper previousApplied = new PreviousAppliedLoansHelper();
            var response = await previousApplied.GetUserLoanDetail(GlobalVariables.USERNAME);
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
