using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankManagement_WPF.ViewModel.Commands
{
    public class SessionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public UserDetailVM VM { get; set; }

        public SessionCommand(UserDetailVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await VM.Logout();
        }
    }
}
