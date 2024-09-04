using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.SubscribeSubjectSpecf
{
    public class SubjectByIdsSpecification : BaseSpecifications<Subject>
    {
        public SubjectByIdsSpecification(List<int> subjectIds)
            : base(s => subjectIds.Contains(s.Id))
        {
            // Optionally, you can include related entities or apply other filters here
        }
    }
}