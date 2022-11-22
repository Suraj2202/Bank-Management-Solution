using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LoginSecurity.JwtToken
{
    public class TokenManager : ITokenManager
    {
        public string GenerateJsonWebToken(string userName)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Bank@Management@1"));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[]
            {
                new Claim("Issuer", "Admin_Suraj"),
                new Claim("Admin", "true"),
                new Claim(JwtRegisteredClaimNames.UniqueName, userName)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                "Admin_Suraj",
                "Client",
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string authToken)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "Admin_Suraj",
                ValidAudience = "Client",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Bank@Management@1"))
            };
        }
    }
}
