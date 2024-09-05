using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.UploadPdfSpecf
{
    public class UploadPdfByTeacherIdSpecification : BaseSpecifications<UploadPdf>
    {
        public UploadPdfByTeacherIdSpecification(string Student_id)
            : base(ss => ss.Teacher_id == Student_id)
        {
            // Optionally, include related entities like Subjects or Teacher if needed
            Includes.Add(ss => ss.Subject);
        }
    }
}