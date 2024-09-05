﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.app
{
    public class Quiz : BaseEntity
    {
        public Quiz()
        {
            Question = new HashSet<Question>();
            Subject = new HashSet<Subject>();
        }

        public int Degree { get; set; }
        public string Teacher_id { get; set; }
        public ICollection<Question> Question { get; set; }
        public ICollection<Subject> Subject { get; set; }
    }
}