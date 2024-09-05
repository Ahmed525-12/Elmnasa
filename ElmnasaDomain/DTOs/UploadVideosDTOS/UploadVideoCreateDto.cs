using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.UploadVideosDTOS
{
    public class UploadVideoCreateDto
    {
        public List<int> SubjectIds { get; set; } // List of Subject IDs

        public string Video_Url { get; set; }
        public string Video_Name { get; set; }
        public string Description { get; set; }
    }
}