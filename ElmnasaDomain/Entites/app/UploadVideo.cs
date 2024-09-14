using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.app
{
    public class UploadVideo : BaseEntity
    {
        public UploadVideo()
        {
            TeacherSubject = new HashSet<TeacherSubject>();
        }

        public string Teacher_id { get; set; }
        public string Video_Url { get; set; }
        public string Video_Name { get; set; }
        public string Description { get; set; }
        public ICollection<TeacherSubject> TeacherSubject { get; set; }
    }
}