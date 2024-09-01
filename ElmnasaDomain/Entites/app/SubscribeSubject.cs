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
            Subject = new HashSet<Subject>();
        }

        public string Teacher_id { get; set; }
        public string Student_id { get; set; }
        public ICollection<Subject> Subject { get; set; }
    }
}