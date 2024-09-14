using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.app
{
    public class TeacherSubject : BaseEntity
    {
        public string Teacher_id { get; set; }

        // Foreign Key for Subject
        public int SubjectId { get; set; }

        // Navigation Property for the related Subject
        public Subject Subject { get; set; }

        public ICollection<SubscribeSubject> SubscribeSubject { get; set; }
        public ICollection<UploadPdf> UploadPdf { get; set; }

        public ICollection<UploadVideo> UploadVideo { get; set; }

        public ICollection<Quiz> Quiz { get; set; }
    }
}