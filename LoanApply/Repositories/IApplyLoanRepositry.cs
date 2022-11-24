using LoanApply.Models.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanApply.Repositories
{
    public interface IApplyLoanRepositry
    {
        Task<LoanDetail> GetLoanAsync(int loanId);
        Task<List<LoanDetail>> GetAllLoanAsync(string userName);
        Task<bool> SaveLoanDeatilAsync(LoanDetail loanDetail);
    }
}
