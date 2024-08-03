using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.app
{
    public class Quiz
    {
        public Quiz()
        {
            Question = new HashSet<Question>();
            Subject = new HashSet<Subject>();
        }

        public string Teacher_id { get; set; }
        private ICollection<Question> Question { get; set; }
        private ICollection<Subject> Subject { get; set; }
    }
}