using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.QuizDTOs
{
    public class QuizCreateDTO
    {
        public int Degree { get; set; }

        public ICollection<Question> Question { get; set; }
        public ICollection<Subject> Subject { get; set; }
    }
}