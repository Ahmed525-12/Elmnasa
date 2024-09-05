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

        public string Question_Name { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public int Degree { get; set; }
    }
}