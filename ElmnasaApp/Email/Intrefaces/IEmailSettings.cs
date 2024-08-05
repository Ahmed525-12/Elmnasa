using ElmnasaDomain.DTOs.EmailDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Email.Intrefaces
{
    public interface IEmailSettings
    {
        public void SendEmail(EmailDTO email);
    }
}