using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.AnswerSpecf
{
    public class AnswerByIdSpecfication : BaseSpecifications<Answer>
    {
        public AnswerByIdSpecfication(List<int> AnswerIds)
            : base(s => AnswerIds.Contains(s.Id))
        {
            // Optionally, you can include related entities or apply other filters here
        }
    }
}