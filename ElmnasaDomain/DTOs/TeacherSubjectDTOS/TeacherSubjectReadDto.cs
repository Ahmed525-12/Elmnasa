using ElmnasaDomain.DTOs.SubjectDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.TeacherSubjectDTOS
{
    public class TeacherSubjectReadDto
    {
        public int Id { get; set; }
        public string Teacher_id { get; set; }
        public SubjectDTO Subject { get; set; }
    }
}