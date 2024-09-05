using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.UploadPdfDTOs
{
    public class UploadPdfCreateDto
    {
        public List<int> SubjectIds { get; set; } // List of Subject IDs

        public string Pdf_Url { get; set; }
        public string PdfName { get; set; }
    }
}