using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.SubjectSpecf
{
    public class UserSubjectSpecf : BaseSpecifications<Subject>
    {
        public UserSubjectSpecf(string userId) : base(p => p.Account_id == userId

            )
        {
        }
    }
}