using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.app
{
    public class UploadPdf
    {
        public UploadPdf()
        {
            Subject = new HashSet<Subject>();
        }

        public string Teacher_id { get; set; }
        public string Pdf_Url { get; set; }
        private ICollection<Subject> Subject { get; set; }
    }
}