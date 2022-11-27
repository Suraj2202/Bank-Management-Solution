using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankManagement_WPF.ViewModel.Commands
{
    class CommentCloseCommand : ICommand
    {
        public CommentVM VM { get; set; }

        public CommentCloseCommand(CommentVM vm)
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
            VM.CloseWindow();
        }
    }
}
