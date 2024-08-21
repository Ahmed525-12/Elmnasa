using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.Account
{
    public class AccountDTO
    {
        public string Email { get; set; }

        public string DisplayName { get; set; }
        public string Token { get; set; }
        public List<string> RoleName { get; set; }
    }
}