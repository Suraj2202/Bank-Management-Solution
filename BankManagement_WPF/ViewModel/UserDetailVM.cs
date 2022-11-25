using BankManagement_WPF.View;
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
    public class UserDetailVM : INotifyPropertyChanged
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


        public string EndDate { get; set; }

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


        public UserDetailsCommand UserDetailsCommand { get; set; }

        public UserDetailVM()
        {
            GetUserDetails();
            UserDetailsCommand = new UserDetailsCommand(this);
        }

        public async void MakeQuery()
        {
            var userDetail = await LoginSecurityHelper.GetUserDetail(UserName);
        }
        public async void GetUserDetails()
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
            DOB = userDetail.DOB.ToString("dd/MM/yyyy");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
