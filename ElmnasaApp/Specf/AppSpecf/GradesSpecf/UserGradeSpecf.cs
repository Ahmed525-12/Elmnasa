using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.GradesSpecf
{
    public class UserGradeSpecf : BaseSpecifications<Grades>
    {
        public UserGradeSpecf(string userId) : base(p => p.Student_id == userId

            )
        {
        }
    }
}