using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.StudentDTOs
{
    public class StudentReigsterDTO
    {
        [Required(ErrorMessage = "Email is requird")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "name is requird")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "password is requird")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public long UId { get; set; }

        [Required(ErrorMessage = "Parent number is required")]
        public long ParentNumber { get; set; }
    }
}