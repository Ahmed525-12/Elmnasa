using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.AnswerDtos
{
    public class UpdateAnswerDto
    {
        public string Name { get; set; }
        public bool isTrue { get; set; }
        public int id { get; set; }
        public string Teacher_id { get; set; }
    }
}