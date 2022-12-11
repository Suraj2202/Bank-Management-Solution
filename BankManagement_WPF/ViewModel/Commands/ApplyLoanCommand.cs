using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankManagement_WPF.ViewModel.Commands
{
    public class ApplyLoanCommand : ICommand
    {
        public ApplyLoanVM VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public ApplyLoanCommand(ApplyLoanVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.CreateNewLoan();
        }
    }
}
