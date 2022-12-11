using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel
{
    public class GlobalVariables
    {
        private static string username;

        public static string USERNAME
        {
            get { return username; }
            set { username = value; }
        }

        private static string comment;

        public static string COMMENT
        {
            get { return comment; }
            set { comment = value; }
        }

        private static int londId;

        public static int LOANID
        {
            get { return londId; }
            set { londId = value; }
        }


    }
}
