using LoginSecurity.Data;
using LoginSecurity.JwtToken;
using LoginSecurity.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BankManagementDbContext bankManagementDbContext;

        ITokenManager _tokenManager = new TokenManager();

        public UserRepository(BankManagementDbContext bankManagementDbContext)
        {
            this.bankManagementDbContext = bankManagementDbContext;
        }

        public async Task<int> ValidateUserCredAsync(string userName, string password)
        {
            UserDetail user = bankManagementDbContext.UserDetails?.Where(x => 
                                            x.UserName == userName &&
                                            x.Password == password)
                                            .FirstOrDefault();

            if (user != null)
            {
                string userToken = user.Token;
                bool valid = _tokenManager.ValidateToken(userToken);

                if (valid)
                    return user.Role;

                else
                {
                    string token = _tokenManager.GenerateJsonWebToken(user.UserName);
                    user.Token = token;
                    await bankManagementDbContext.SaveChangesAsync();
                    return user.Role;
                }
            }
            else
                return 2;
        }
        public async Task<UserDetail> GetUserAsync(string userName)
        {
           return await bankManagementDbContext.UserDetails?.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<bool> SaveUserDeatilAsync(UserDetail userDetail)
        {
            try
            {
                await bankManagementDbContext.UserDetails.AddAsync(userDetail);
                await bankManagementDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> EndSessionAsync(string userName)
        {
            UserDetail userDetail = await GetUserAsync(userName);
            if(userDetail != null)
            {
                userDetail.Token = "logout";
                await bankManagementDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ValidateUserSessionAsync(string userName)
        {
            UserDetail userDetail = await GetUserAsync(userName);
            if (userDetail != null)
            {
                return _tokenManager.ValidateToken(userDetail.Token);
            }
            return false;
        }

        public async Task<bool> UpdateUserDeatilAsync(string userName, UserDetail userDetail)
        {
            try
            {
                UserDetail user = await GetUserAsync(userName);
                if (user != null)
                {

                    user.Address = userDetail.Address;
                    user.Contact = userDetail.Contact;
                    user.Country = userDetail.Country;
                    user.DOB = userDetail.DOB;
                    user.Email = userDetail.Email;
                    user.Name = userDetail.Name;
                    user.PAN = userDetail.PAN;
                    user.Password = userDetail.Password;
                    user.State = userDetail.State;

                    bankManagementDbContext.UserDetails.Update(user);
                    await bankManagementDbContext.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
