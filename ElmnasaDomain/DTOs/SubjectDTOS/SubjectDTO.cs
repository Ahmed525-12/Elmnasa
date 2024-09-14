using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.SubjectDTOS
{
    public class SubjectDTO
    {
        [Required(ErrorMessage = "Subject Name is required ")]
        public string Subject_Name { get; set; }
    }
}