using BankManagement_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel.Helpers
{
    interface IPreviousAppliedLoansHelper
    {
         Task<IEnumerable<LoanDetail>> GetAdminLoanDetail();
         Task<IEnumerable<LoanDetail>> GetUserLoanDetail(string userName);
    }
}
