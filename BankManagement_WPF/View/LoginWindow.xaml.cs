using BankManagement_WPF.View.Admin;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //For User Login

            DashboardWindow dashboard = new DashboardWindow();
            dashboard.Show();
            this.Close();

            /*
                        //For Admin Login

                        AdminDashboardWindow dashboard = new AdminDashboardWindow();
                        dashboard.Show();
                        this.Close();
            */
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            SignupWindow signupWindow = new SignupWindow();
            signupWindow.ShowDialog();
        }
    }
}
