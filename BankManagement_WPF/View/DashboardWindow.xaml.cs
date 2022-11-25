using BankManagement_WPF.ViewModel;
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
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
        }


        private void ApplyLoanButton_Click(object sender, RoutedEventArgs e)
        {
            new ApplyLoanWindow().ShowDialog();
        }
        private void UpdateDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            new UpdateUserDetailWindow().ShowDialog();
        }
        private void PreviousLoanButton_Click(object sender, RoutedEventArgs e)
        {
            new PreviousAppliedLoansWindow().ShowDialog();
        }
        
    }
}
