using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.app
{
    public class Question : BaseEntity
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public string Teacher_id { get; set; }
        public string Question_Name { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<Quiz> Quiz { get; set; }
        public int Degree { get; set; }
    }
}