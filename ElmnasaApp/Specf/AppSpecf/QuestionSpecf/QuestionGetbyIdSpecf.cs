using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.QuestionSpecf
{
    public class QuestionGetbyIdSpecf : BaseSpecifications<Question>
    {
        public QuestionGetbyIdSpecf(int id)
            : base(ss => ss.Id == id)
        {
            // Optionally, include related entities like Subjects or Teacher if needed
            Includes.Add(ss => ss.Answers);
        }
    }
}