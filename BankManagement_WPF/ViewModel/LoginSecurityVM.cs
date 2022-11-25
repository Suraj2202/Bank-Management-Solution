using BankManagement_WPF.Model.RequestData;
using BankManagement_WPF.View;
using BankManagement_WPF.View.Admin;
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
    class LoginSecurityVM : INotifyPropertyChanged
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set 
            {
                passWord = value;
                OnPropertyChanged("PassWord");
            }
        }

        private string warning;

        public string Warning
        {
            get { return warning; }
            set 
            { 
                warning = value;
                OnPropertyChanged("Warning");
            }
        }


        public LoginSecurityCommand LoginSecurityCommand { get; set; }
        public SignupCommand SignupCommand { get; set; }

        public LoginSecurityVM()
        {
            LoginSecurityCommand = new LoginSecurityCommand(this);
            SignupCommand = new SignupCommand(this);
        }

        public async void MakeQuery()
        {
            string agent = await LoginSecurityHelper.LoginAgent(new LoginDetail { UserName = UserName, Password = PassWord });

            if(agent == "User")
            {
                //For User Login

                DashboardWindow dashboard = new DashboardWindow();
                dashboard.ShowDialog();

            }
            else if(agent == "Admin")
            {
                AdminDashboardWindow dashboard = new AdminDashboardWindow();
                dashboard.ShowDialog();
            }
            else
            {
                Warning = "Something Went Wrong !!!";
            }
        }

        public void OpenSignupWindow()
        {
            SignupWindow signupWindow = new SignupWindow();
            signupWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
