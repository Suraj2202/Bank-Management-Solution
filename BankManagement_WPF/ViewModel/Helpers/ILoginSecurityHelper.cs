using BankManagement_WPF.Model;
using BankManagement_WPF.Model.RequestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel.Helpers
{
    public interface ILoginSecurityHelper
    {
         Task<UserDetail> GetUserDetail(string userName);
         Task<string> LoginAgent(LoginDetail loginDetail);
    }
}
