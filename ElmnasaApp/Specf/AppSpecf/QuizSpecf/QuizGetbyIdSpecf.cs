using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.QuizSpecf
{
    public class QuizGetbyIdSpecf : BaseSpecifications<Quiz>
    {
        public QuizGetbyIdSpecf(int id)
            : base(ss => ss.Id == id)
        {
            // Optionally, include related entities like Subjects or Teacher if needed
            Includes.Add(ss => ss.Question);
            Includes.Add(ss => ss.TeacherSubject);
        }
    }
}