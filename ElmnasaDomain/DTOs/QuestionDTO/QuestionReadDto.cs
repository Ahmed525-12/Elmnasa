using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.QuestionDTO
{
    public class QuestionReadDto
    {
        public int Id { get; set; }
        public string Question_Name { get; set; }
        public string Teacher_id { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public int Degree { get; set; }
    }
}