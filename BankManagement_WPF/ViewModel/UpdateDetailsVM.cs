using BankManagement_WPF.Model;
using BankManagement_WPF.Validations;
using BankManagement_WPF.ViewModel.Commands;
using BankManagement_WPF.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel
{
    public class UpdateDetailsVM : INotifyPropertyChanged
    {
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
                OnPropertyChanged("userName");
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
            }
        }

        private string dob;

        public string DOB
        {
            get { return dob; }
            set
            {
                dob = value;
                OnPropertyChanged("DOB");
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


        public event PropertyChangedEventHandler PropertyChanged;
        TextBlockValidation textBlockValidation;

        public UpdateUserDetailCommand UpdateUserDetailCommand { get; set; }

        public UpdateDetailsVM()
        {
            GetUserDetails();
            textBlockValidation = new TextBlockValidation();
            UpdateUserDetailCommand = new UpdateUserDetailCommand(this);
        }

        private async void GetUserDetails()
        {
            var userDetail = await LoginSecurityHelper.GetUserDetail(GlobalVariables.USERNAME);

            UserName = userDetail.UserName;
            PassWord = userDetail.Password;
            Name = userDetail.Name;
            Address = userDetail.Address;
            State = userDetail.State;
            Country = userDetail.Country;
            EmailId = userDetail.Email;
            PAN = userDetail.PAN.ToString();
            ContactNo = userDetail.Contact.ToString();
            DOB = userDetail.DOB.ToString("MM/dd/yyyy");
            AccountType = userDetail.AccountType;
        }

        public async void UpdateUserDetails()
        {
            //Validation:

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(PassWord) || string.IsNullOrEmpty(EmailId) || string.IsNullOrEmpty(PAN) || string.IsNullOrEmpty(ContactNo) || string.IsNullOrEmpty(DOB))
            {
                Warning = "All Fields are mandatory";
                return;
            }

            if (textBlockValidation.UserNameValidation(UserName))
            {
                Warning = " 7 < UserName > 21 and, must not have special character except _";
                return;
            }

            if (!textBlockValidation.PasswordValidation(PassWord))
            {
                Warning = "Password must be inbetween 8-20 and must have 1 Caps, 1 Small and 1 Special character";
                return;
            }

            if (!textBlockValidation.EmailIDValidation(EmailId))
            {
                Warning = "Invalid Email ID";
                return;
            }

            if (!textBlockValidation.PANandContactNoValidation(PAN))
            {
                Warning = "Invalid PAN Number, 1st digit should not be 0 and must have 10 digits.";
                return;
            }

            if (!textBlockValidation.PANandContactNoValidation(ContactNo))
            {
                Warning = "Invalid Contact Number, 1st digit should not be 0 and must have 10 digits.";
                return;
            }

            if (textBlockValidation.AgeGreaterThan18(DOB))
            {
                Warning = "No Future Date Please and Age > 18.";
                return;
            }

            string val = DOB.Contains("-") ? "-" : "/";
            string[] dates = DOB.Split(" ")[0].Split(val);
            string myDate = dates[1] + "/" + dates[0] + "/" + dates[2];

            UserDetail user = new UserDetail()
            {
                Name = Name,
                Password = PassWord,
                Address = Address,
                State = State,
                Country = Country,
                Email = EmailId,
                PAN = long.Parse(PAN),
                Contact = long.Parse(contactNo),
                DOB = DateTime.Parse(myDate),
            };

            string updateStatus = await UpdateDetailHelper.UpdateUserDetail(GlobalVariables.USERNAME, user);

            Warning = updateStatus;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
