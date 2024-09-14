using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.TeacherSubjectsSpecf
{
    public class TeacherSubjectByTeacherIdSpecification : BaseSpecifications<TeacherSubject>
    {
        public TeacherSubjectByTeacherIdSpecification(string Teacher_id)
            : base(ss => ss.Teacher_id == Teacher_id)
        {
            // Optionally, include related entities like Subjects or Teacher if needed
            Includes.Add(ss => ss.Subject);
        }
    }
}