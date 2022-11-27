using BankManagement_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankManagement_WPF.View
{
    /// <summary>
    /// Interaction logic for PreviousAppliedLoansWindow.xaml
    /// </summary>
    public partial class PreviousAppliedLoansWindow : Window
    {
        public PreviousAppliedLoansWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            LoanDetail row = dg.SelectedItem as LoanDetail;
            ViewModel.GlobalVariables.LOANID = row.LoanId;
        }
    }
}
