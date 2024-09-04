using ElmnasaDomain.DTOs.SubjectDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.SubscribeSubjectDTO
{
    public class SubscribeSubjectReadDto
    {
        public int Id { get; set; }
        public string Student_id { get; set; }
        public List<SubjectDTO> Subjects { get; set; } // List of Subject DTOs
    }
}