using LoginSecurity.Models.Domains;
using System.Threading.Tasks;

namespace LoginSecurity.Repositories
{
    public interface IUserRepository
    {
        Task<int> ValidateUserCredAsync(string userName, string password);
        Task<UserDetail> GetUserAsync(string userName);
        Task<bool> SaveUserDeatilAsync(UserDetail userDetail);
        Task<bool> UpdateUserDeatilAsync(string userName, UserDetail userDetail);
        Task<bool> EndSessionAsync(string userName);
        Task<bool> ValidateUserSessionAsync(string userName);
    }
}