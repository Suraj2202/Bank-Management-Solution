using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankManagement_WPF.ViewModel.Commands
{
    class LoginSecurityCommand : ICommand
    {
        public LoginSecurityVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LoginSecurityCommand(LoginSecurityVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            string userName = parameter as string;

            if (string.IsNullOrWhiteSpace(userName))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }
    }
}
