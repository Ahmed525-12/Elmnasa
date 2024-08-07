using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.TeacherDtos
{
    public class TeacherDto
    {
        public string Email { get; set; }

        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Teacher_Image { get; set; }
    }
}