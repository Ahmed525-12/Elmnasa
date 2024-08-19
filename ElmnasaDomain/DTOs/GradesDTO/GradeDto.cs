using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.GradesDTO
{
    public class GradeDto
    {
        [AllowedValues("1", "2", "3", "4", "5", "6")]
        public string Name { get; set; }

        public string Student_id { get; set; }
    }
}