using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.StudentDTOs
{
    public class StudentDto
    {
        public string Email { get; set; }

        public long Uid { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}