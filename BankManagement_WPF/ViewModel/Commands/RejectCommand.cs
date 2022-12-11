using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankManagement_WPF.ViewModel.Commands
{
    public class RejectCommand : ICommand
    {
        public AdminDashboardVM VM { get; set; }

        public RejectCommand(AdminDashboardVM vm)
        {
            VM = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.RejectCommand();
        }
    }
}
