﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.app
{
    public class Answer : BaseEntity
    {
        public string Name { get; set; }
        public bool isTrue { get; set; }
        public string Teacher_id { get; set; }

        [JsonIgnore]
        public ICollection<Question> Question { get; set; }
    }
}