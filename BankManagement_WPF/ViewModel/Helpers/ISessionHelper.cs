using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel.Helpers
{
     interface ISessionHelper
    {
         Task<bool> ValidateToken(string userName);
         Task<string> LogoutUser(string userName);
    }
}
