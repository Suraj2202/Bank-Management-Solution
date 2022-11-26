using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel
{
    class GlobalVariables
    {
        private static string username;

        public static string USERNAME
        {
            get { return username; }
            set { username = value; }
        }

        private static string jwtToken;

        public static string JWTTOKEN
        {
            get { return jwtToken; }
            set { jwtToken = value; }
        }

        private int londId;

        public int LOANID
        {
            get { return londId; }
            set { londId = value; }
        }


    }
}
