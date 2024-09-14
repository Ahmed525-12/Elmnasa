using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.app
{
    public class SubscribeSubject : BaseEntity
    {
        public SubscribeSubject()
        {
            TeacherSubject = new HashSet<TeacherSubject>();
        }

        public string Student_id { get; set; }
        public ICollection<TeacherSubject> TeacherSubject { get; set; }
    }
}