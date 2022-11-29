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
    class CommentVM : INotifyPropertyChanged
    {
        private string warning;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Warning
        {
            get { return warning; }
            set { warning = value; OnPropertyChanged("Warning"); }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set 
            {
                comment = value;
                GlobalVariables.COMMENT = comment;
                OnPropertyChanged("Warning");
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CommentCloseCommand CommentCloseCommand { get; set; }
        public CommentVM()
        {
            CommentCloseCommand = new CommentCloseCommand(this);
        }

        public async void CloseWindow()
        {
            if (!string.IsNullOrEmpty(Comment))
            {
                IUpdateDetailHelper update = new UpdateDetailHelper();
                await update.UpdateLoanComment(GlobalVariables.LOANID, GlobalVariables.COMMENT);
                Application.Current.Windows[Application.Current.Windows.Count - 2].Close();
            }
            else
                Warning = "Please Enter Comment";
        }
    }
}
