using ElmnasaApp.Email.Intrefaces;
using ElmnasaApp.Email.WorkEmail;
using ElmnasaApp.JWTToken.Intrefaces;
using ElmnasaApp.JWTToken.WorkToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.ServicesExtension
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<IEmailSettings, EmailSettings>();

            // Register other managers similarly

            Services.AddAuthentication(Option =>
            {
                Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(option =>
                    {
                        option.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = _configuration["JWT:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = _configuration["JWT:Audience"],
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
                        };
                    });
            return Services;
        }
    }
}