using BankManagement_WPF.Model.RequestData;
using BankManagement_WPF.View;
using BankManagement_WPF.View.Admin;
using BankManagement_WPF.ViewModel.Commands;
using BankManagement_WPF.ViewModel.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankManagement_WPF.ViewModel
{
    class LoginSecurityVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> propertyErrors = new Dictionary<string, List<string>>();
        private string userName;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");

                ClearErrors(nameof(UserName));

                var list = new[] { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "=", "\"" };
                bool res = list.Any(value.Contains);
                if(res)
                {
                    AddError(nameof(userName), "Invalid User Name. It must not contain any Special charachter except underscore(_)");
                }
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
            //validation
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(PassWord))
                return;
            try
            {
                string agent = await LoginSecurityHelper.LoginAgent(new LoginDetail { UserName = UserName, Password = PassWord });

                if (agent == "User")
                {
                    Warning = "";
                    //For User Login
                    GlobalVariables.USERNAME = UserName;
                    DashboardWindow dashboard = new DashboardWindow();
                    dashboard.ShowDialog();
                }
                else if (agent == "Admin")
                {
                    GlobalVariables.USERNAME = "admin";
                    AdminDashboardWindow dashboard = new AdminDashboardWindow();
                    dashboard.ShowDialog();
                }
                else
                {
                    Warning = "Something Went Wrong !!!";
                }
            }
            catch(Exception)
            {
                Warning = "Report to Administration.";
            }

        }

        public void OpenSignupWindow()
        {
            SignupWindow signupWindow = new SignupWindow();
            signupWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Error Handling
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool HasErrors => propertyErrors.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            return propertyErrors.GetValueOrDefault(propertyName, new List<string>());
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if(!propertyErrors.ContainsKey(propertyName))
            {
                propertyErrors.Add(propertyName, new List<string>());
            }

            propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertName));
        }

        private void ClearErrors(string propertyName)
        {
 //           propertyErrors.Clear();
            OnErrorsChanged(propertyName);
        }
    }
}
