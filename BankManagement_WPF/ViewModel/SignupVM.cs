using BankManagement_WPF.Model;
using BankManagement_WPF.ViewModel.Commands;
using BankManagement_WPF.ViewModel.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel
{
    class SignupVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Dictionary<string, string> propertyErrors;
        TextBlockValidation textBlockValidation;

        #region Value Property
        public Dictionary<string, string> PropertyErrors
        {
            get { return propertyErrors; }
            set { propertyErrors = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
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

        private string password;

        public string PassWord
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("PassWord");

                ClearErrors(nameof(PassWord));
                bool res = textBlockValidation.PasswordValidation(value);
                if (res)
                    AddError(nameof(PassWord), "Password must be inbetween 8-20 and must have 1 Caps, 1 Small and 1 Special character");

            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");

                ClearErrors(nameof(Address));

            }
        }

        private string state;

        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("State");
                
                ClearErrors(nameof(State));

            }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged("Country");

                ClearErrors(nameof(Country));

            }
        }

        private string emailId;

        public string EmailId
        {
            get { return emailId; }
            set
            {
                emailId = value;
                OnPropertyChanged("EmailId");

                ClearErrors(nameof(EmailId));
                bool res = textBlockValidation.EmailIDValidation(value);
                if (res)
                    AddError(nameof(EmailId), "Invalid Email ID");

            }
        }

        private string pan;

        public string PAN
        {
            get { return pan; }
            set
            {
                pan = value;
                OnPropertyChanged("PAN");

                ClearErrors(nameof(PAN));
                bool res = textBlockValidation.PANandContactNoValidation(value);
                if (res)
                    AddError(nameof(PAN), "Invalid PAN Number, 1st digit should not be 0 and must have 10 digits.");

            }
        }

        private string contactNo;

        public string ContactNo
        {
            get { return contactNo; }
            set
            {
                contactNo = value;
                OnPropertyChanged("ContactNo");

                ClearErrors(nameof(ContactNo));
                bool res = textBlockValidation.PANandContactNoValidation(value);
                if (res)
                    AddError(nameof(ContactNo), "Invalid Contact Number, 1st digit should not be 0 and must have 10 digits.");

            }
        }


        public string EndDate { get; set; }

        private string dob;

        public string DOB
        {
            get { return dob; }
            set
            {
                dob = value;
                OnPropertyChanged("DOB");

                ClearErrors(nameof(DOB));
                bool res = textBlockValidation.AgeGreaterThan18(value);
                if (res)
                    AddError(nameof(DOB), "No Future Date Please and Age > 18.");

            }
        }

        private string accountType;

        public string AccountType
        {
            get { return accountType; }
            set
            {
                accountType = value;
                OnPropertyChanged("AccountType");
            }
        }

        private string warning;

        public string Warning
        {
            get { return warning; }
            set { warning = value; OnPropertyChanged("Warning"); }
        }

        public CreateAccountCommand CreateAccountCommand { get; set; }

        #endregion


        public SignupVM()
        {
            propertyErrors = new Dictionary<string, string>();
            EndDate = DateTime.Now.ToString("dd/MM/yyyy");
            CreateAccountCommand = new CreateAccountCommand(this);
            textBlockValidation = new TextBlockValidation();
        }

        public async void CreateNewAccount()
        {
            #region Validation:

            CheckForIsNullOrEmpty(nameof(Name), Name);
            CheckForIsNullOrEmpty(nameof(UserName), UserName);
            CheckForIsNullOrEmpty(nameof(PassWord), PassWord);
            CheckForIsNullOrEmpty(nameof(Address), Address);
            CheckForIsNullOrEmpty(nameof(State), State);
            CheckForIsNullOrEmpty(nameof(Country), Country);
            CheckForIsNullOrEmpty(nameof(EmailId), EmailId);
            CheckForIsNullOrEmpty(nameof(PAN), PAN);
            CheckForIsNullOrEmpty(nameof(ContactNo), ContactNo);
            CheckForIsNullOrEmpty(nameof(DOB), DOB);

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(PassWord) || string.IsNullOrEmpty(EmailId) || string.IsNullOrEmpty(PAN) || string.IsNullOrEmpty(ContactNo) || string.IsNullOrEmpty(DOB))
            {
                Warning = "All Fields are mandatory";
                return;
            }

            if(textBlockValidation.UserNameValidation(UserName))
            {
                Warning = " 7 < UserName > 21 and, must not have special character except _";
                return;
            }

            if(textBlockValidation.PasswordValidation(PassWord))
            {
                Warning = "Password must be inbetween 8-20 and must have 1 Caps, 1 Small and 1 Special character";
                return;
            }

            if(textBlockValidation.EmailIDValidation(EmailId))
            {
                Warning = "Invalid Email ID";
                return;
            }

            if (textBlockValidation.PANandContactNoValidation(PAN))
            {
                Warning = "Invalid PAN Number, 1st digit should not be 0 and must have 10 digits.";
                return;
            }

            if (textBlockValidation.PANandContactNoValidation(ContactNo))
            {
                Warning = "Invalid Contact Number, 1st digit should not be 0 and must have 10 digits.";
                return;
            }

            if(textBlockValidation.AgeGreaterThan18(DOB))
            {
                Warning = "No Future Date Please and Age > 18.";
                return;
            }

            #endregion

            string val = DOB.Contains("-") ? "-" : "/";
            string[] dates= DOB.Split(" ")[0].Split(val);
            string myDate = dates[1]+ "/" + dates[0]+ "/" + dates[2];

            UserDetail user = new UserDetail()
            {
                Name = Name,
                UserName = UserName,
                Password = PassWord,
                Address = Address,
                State = State,
                Country = Country,
                Email = EmailId,
                PAN = long.Parse(PAN),
                Contact = long.Parse(contactNo),
                DOB = DateTime.Parse(myDate),
                AccountType = AccountType.Split(":")[1].Trim()
            };

            ISignupHelper signup = new SignupHelper();
            string createAccountStatus = await signup.CreateAccount(user);

            if (createAccountStatus == "Added Succesfully")
            {
                Warning = "Your Account Created Successfully";
                System.Windows.MessageBox.Show(Warning);
            }
            else if (createAccountStatus == "User Already Exists")
            {
                Warning = "User Name is already taken";
                System.Windows.MessageBox.Show(Warning);
            }
            else
            {
                Warning = "Something went wrong !!!";
                System.Windows.MessageBox.Show(Warning);
            }
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
            if (!propertyErrors.ContainsKey(propertyName))
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
            propertyErrors?.Remove(propertyName);
            OnPropertyChanged("PropertyErrors");
            OnErrorsChanged(propertyName);
        }

        private void CheckForIsNullOrEmpty(string propertyName, string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                ClearErrors(propertyName);
                AddError(propertyName, propertyName + "Field is mandatory.");
            }
        }

        #endregion
    }
}
