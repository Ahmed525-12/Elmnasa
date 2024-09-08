using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.QuestionSpecf
{
    public class QuestionGetbyIdSpecfForQuiz : BaseSpecifications<Question>
    {
        public QuestionGetbyIdSpecfForQuiz(IEnumerable<int> id)
            : base(q => id.Contains(q.Id))
        {
            // Optionally, include related entities like Subjects or Teacher if needed
            Includes.Add(ss => ss.Answers);
        }
    }
}