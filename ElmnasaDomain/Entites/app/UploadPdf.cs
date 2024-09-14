using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.app
{
    public class UploadPdf : BaseEntity
    {
        public UploadPdf()
        {
            TeacherSubject = new HashSet<TeacherSubject>();
        }

        public string Teacher_id { get; set; }
        public string Pdf_Url { get; set; }
        public string PdfName { get; set; }
        public ICollection<TeacherSubject> TeacherSubject { get; set; }
    }
}