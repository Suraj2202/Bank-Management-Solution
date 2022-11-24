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
    /// Interaction logic for ApplyLoanWindow.xaml
    /// </summary>
    public partial class ApplyLoanWindow : Window
    {
        public ApplyLoanWindow()
        {
            InitializeComponent();
        }
        
        private void PreviousAppliedButton_Click(object sender, RoutedEventArgs e)
        {
            new PreviousAppliedLoansWindow().Show();
        }
    }

}
