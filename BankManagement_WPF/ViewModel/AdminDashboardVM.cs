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

        public IEnumerable<LoanDetail> originalList { get; set; }

        private string search;

        public string Search
        {
            get { return search; }
            set 
            {
                search = value; 
                if(search!=null)
                {
                    IEnumerable<LoanDetail> filteredList = originalList?.Where(x => x.Status.ToLower().Contains(search.ToLower()));
                    LoanDetails.Clear();
                    LoanDetails.AddRange(filteredList);
                    loanDetails.Refresh();
                }
                OnPropertyChanged("Search");
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
            originalList = response;
            LoanDetails = new BindableCollection<LoanDetail>(originalList);
        }

        public async void ApproveCommand()
        {
            Search = "";
            string checkValue = LoanDetails.FirstOrDefault(x => x.LoanId == GlobalVariables.LOANID).Status;
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
            Search = "";
            string checkValue = LoanDetails.FirstOrDefault(x => x.LoanId == GlobalVariables.LOANID).Status;
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
