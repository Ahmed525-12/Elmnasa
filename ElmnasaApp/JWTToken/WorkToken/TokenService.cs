using ElmnasaApp.JWTToken.Intrefaces;
using ElmnasaDomain.Entites.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.JWTToken.WorkToken
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Account> _accountManager;

        public TokenService(IConfiguration configuration, UserManager<Account> accountManager)
        {
            _configuration = configuration;
            _accountManager = accountManager;
        }

        public async Task<string> CreateToken(Account user)
        {
            // Fetch user roles from the account manager
            var roles = await _accountManager.GetRolesAsync(user);
            // Claims
            var UserClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.DisplayName),
            };
            // Add role claims
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    UserClaim.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            // Security Key
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            // Create Token Object
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:Expire"])),
                claims: UserClaim,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}