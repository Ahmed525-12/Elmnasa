using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.AnswerDtos
{
    public class AnswerDTO
    {
        [Required(ErrorMessage = "Answer Name is required ")]
        public string Name { get; set; }

        public bool isTrue { get; set; } = false;
    }
}