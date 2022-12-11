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
    public class LoginSecurityVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        //private readonly Dictionary<string, List<string>> propertyErrors;
        private Dictionary<string, string> propertyErrors;
        TextBlockValidation textBlockValidation;

        public Dictionary<string, string> PropertyErrors
        {
            get { return propertyErrors; }
            set { propertyErrors = value;}
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");

                ClearErrors(nameof(UserName));
                bool res = textBlockValidation.UserNameValidation(value);                
                if (res)
                    AddError(nameof(UserName), "Invalid User Name. It must not contain any Special charachter except underscore(_)");                

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
                
                ClearErrors(nameof(PassWord));
                bool res = textBlockValidation.PasswordValidation(value);
                if (res)
                    AddError(nameof(PassWord), "Password must be inbetween 8-20 and must have 1 Caps, 1 Small and 1 Special character");


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
            propertyErrors = new Dictionary<string, string>();
            textBlockValidation = new TextBlockValidation();
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
                LoginSecurityHelper securityHelper = new LoginSecurityHelper();
                string agent = await securityHelper.LoginAgent(new LoginDetail { UserName = UserName, Password = PassWord });

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
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        #region Error Handling

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => propertyErrors.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            return propertyErrors.GetValueOrDefault(propertyName, "");
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if(!propertyErrors.ContainsKey(propertyName))
            {
                propertyErrors.Add(propertyName, errorMessage);
                OnPropertyChanged("PropertyErrors");
            }
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertName));
        }

        private void ClearErrors(string propertyName)
        {
            propertyErrors.Remove(propertyName);
            OnPropertyChanged("PropertyErrors");
            OnErrorsChanged(propertyName);
        }

        #endregion
    }
}
