using BankManagement_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel.Helpers
{
    public interface IUpdateDetailHelper
    {
        Task<string> UpdateLoanStatus(int loanId, string statusValue);
        Task<string> UpdateUserDetail(string username, UserDetail userDetail);
        Task<string> UpdateLoanComment(int loanId, string commentValue);
    }
}
