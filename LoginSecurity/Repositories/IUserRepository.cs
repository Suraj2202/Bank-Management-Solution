using LoginSecurity.Models.Domains;
using System.Threading.Tasks;

namespace LoginSecurity.Repositories
{
    public interface IUserRepository
    {
        Task<int> ValidateUserCredAsync(string userName, string password);
        Task<UserDetail> GetUserAsync(string userName);
    }
}