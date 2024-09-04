using ElmnasaApp.Specf.Intrefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElmnasaDomain.Entites.app;

namespace ElmnasaApp.Specf.AppSpecf.SubscribeSubjectSpecf
{
    public class SubscribeSubjectByStudentIdSpecification : BaseSpecifications<SubscribeSubject>
    {
        public SubscribeSubjectByStudentIdSpecification(string Student_id)
            : base(ss => ss.Student_id == Student_id)
        {
            // Optionally, include related entities like Subjects or Teacher if needed
            Includes.Add(ss => ss.Subject);
        }
    }
}