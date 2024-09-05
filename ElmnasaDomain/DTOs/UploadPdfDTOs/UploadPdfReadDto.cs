using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.UploadPdfDTOs
{
    public class UploadPdfReadDto
    {
        public int Id { get; set; }
        public List<int> SubjectIds { get; set; } // List of Subject IDs
        public string Teacher_id { get; set; }
        public string Pdf_Url { get; set; }
        public string PdfName { get; set; }
    }
}