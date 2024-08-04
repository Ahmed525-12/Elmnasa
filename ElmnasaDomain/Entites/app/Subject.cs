using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ElmnasaDomain.Entites.app
{
    public class Subject : BaseEntity
    {
        public string Subject_Name { get; set; }
        public string Teacher_id { get; set; }
        public int SubscribeSubjectId { get; set; }

        [ForeignKey("SubscribeSubjectId")]
        public SubscribeSubject SubscribeSubject { get; set; }

        public int UploadPdfId { get; set; }

        [ForeignKey("UploadPdfId")]
        public UploadPdf UploadPdf { get; set; }

        public int UploadVideoId { get; set; }

        [ForeignKey("UploadVideoId")]
        public UploadVideo UploadVideo { get; set; }

        public int QuizId { get; set; }

        [ForeignKey("QuizId")]
        public Quiz Quiz { get; set; }
    }
}