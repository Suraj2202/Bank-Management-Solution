using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankManagement_WPF.ViewModel.Commands
{
    class PreviousAppliedLoansCommand : ICommand
    {
        public ApplyLoanVM VM { get; set; }

        public PreviousAppliedLoansCommand(ApplyLoanVM vm)
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
            VM.PreviousAppliedLoanWindowOpen();
        }
    }
}
