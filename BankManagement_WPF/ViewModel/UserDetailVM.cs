using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel
{
    class UserDetailVM : INotifyPropertyChanged
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


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
