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
            set { passWord = value; }
        }

        public UserDetailsCommand UserDetailsCommand { get; set; }

        public UserDetailVM()
        {
            UserDetailsCommand = new UserDetailsCommand(this);
        }

        public async void MakeQuery()
        {
            var userDetail = await LoginSecurityHelper.GetUserDetail(UserName);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
