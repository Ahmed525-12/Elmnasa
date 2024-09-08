using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.QuestionDTO
{
    public class QuestionCreateDto
    {
        public string Question_Name { get; set; }

        // Change this to a list of Answer IDs (integers)
        public ICollection<int> AnswerIds { get; set; }

        public int Degree { get; set; }
    }
}