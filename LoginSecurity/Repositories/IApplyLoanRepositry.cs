using LoginSecurity.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Repositories
{
    public interface IApplyLoanRepositry
    {
        Task<LoanDetail> GetLoanAsync(int loanId);
        Task<List<LoanDetail>> GetAllLoanAsync(string userName);
        Task<bool> SaveLoanDeatilAsync(LoanDetail loanDetail);
    }
}
