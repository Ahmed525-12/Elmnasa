using ElmnasaDomain.Entites.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.JWTToken.Intrefaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(Account user);
    }
}