using LoginSecurity.Models.Domains;
using System.Threading.Tasks;

namespace LoginSecurity.Repositories
{
    public interface IUserRepository
    {
        Task<int> ValidateUserCredAsync(string userName, string password);
        UserDetail GetUser(string userName);
    }
}