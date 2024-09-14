using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.UploadVideoSpecf
{
    public class UploadVideoByTeacherIdSpecification : BaseSpecifications<UploadVideo>
    {
        public UploadVideoByTeacherIdSpecification(string teacherId)
          : base(uv => uv.Teacher_id == teacherId) // Assuming `Teacher_id` exists in `UploadVideo`
        {
            // Optionally, include related entities like Subjects or Teacher if needed
            Includes.Add(uv => uv.TeacherSubject); // Assuming `UploadVideo` has a related `Subject` entity
        }
    }
}