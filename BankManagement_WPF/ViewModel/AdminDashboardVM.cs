using BankManagement_WPF.Model;
using BankManagement_WPF.View;
using BankManagement_WPF.ViewModel.Commands;
using BankManagement_WPF.ViewModel.Helpers;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace BankManagement_WPF.ViewModel
{
    public class AdminDashboardVM : INotifyPropertyChanged
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

        public OpenCommentCommand OpenCommentCommand { get; set; }
        public ApprovedStatusCommand ApprovedStatusCommand { get; set; }
        public RejectCommand RejectCommands { get; set; }

        public AdminDashboardVM()
        {
            OpenCommentCommand = new OpenCommentCommand(this);
            ApprovedStatusCommand = new ApprovedStatusCommand(this);
            RejectCommands = new RejectCommand(this);
            if(!string.IsNullOrEmpty(GlobalVariables.COMMENT))
            {
                CommentCommand();
            }

            DisplayAllAttributes();
        }

        public async void CommentCommand()
        {
            IUpdateDetailHelper updateDetail = new UpdateDetailHelper();
            await updateDetail.UpdateLoanComment(GlobalVariables.LOANID, GlobalVariables.COMMENT);
        }

        public void OpenCommentWindow()
        {
            new CommentWindow().ShowDialog();
        }

        private async void DisplayAllAttributes()
        {
            IPreviousAppliedLoansHelper previousAppliedLoans = new PreviousAppliedLoansHelper();
            var response = await previousAppliedLoans.GetAdminLoanDetail();
            LoanDetails = new BindableCollection<LoanDetail>(response);
        }

        public async void ApproveCommand()
        {
            int pos = GlobalVariables.LOANID > 1000 ? GlobalVariables.LOANID-1001: GlobalVariables.LOANID - 1;
            string checkValue = LoanDetails[pos].Status;
            if (checkValue != "Pending")
            {
                System.Windows.MessageBox.Show("Can't Change the Status");
                return;
            }
            IUpdateDetailHelper updateDetailHelper = new UpdateDetailHelper();
            await updateDetailHelper.UpdateLoanStatus(GlobalVariables.LOANID, "APPROVED");
            DisplayAllAttributes();
            GlobalVariables.COMMENT = "";
            new CommentWindow().ShowDialog();
        }

        public async void RejectCommand()
        {
            int pos = GlobalVariables.LOANID > 1000 ? GlobalVariables.LOANID - 1001 : GlobalVariables.LOANID - 1;
            string checkValue = LoanDetails[pos].Status;
            if (checkValue != "Pending")
            {
                System.Windows.MessageBox.Show("Can't Change the Status");
                return;
            }
            IUpdateDetailHelper update = new UpdateDetailHelper();
            await update.UpdateLoanStatus(GlobalVariables.LOANID, "REJECTED");
            DisplayAllAttributes();
            GlobalVariables.COMMENT = "";
            new CommentWindow().ShowDialog();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
