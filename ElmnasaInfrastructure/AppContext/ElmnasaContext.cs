using ElmnasaDomain.Entites.app;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ElmnasaInfrastructure.AppContext
{
    public class ElmnasaContext : DbContext
    {
        public ElmnasaContext(DbContextOptions<ElmnasaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TeacherSubject>()
                .HasMany(SS => SS.SubscribeSubject)
                .WithMany(S => S.TeacherSubject)
                .UsingEntity(j => j.ToTable("StudentSubscribeTeacherSubject"))

           ;

            modelBuilder.Entity<TeacherSubject>()
             .HasMany(SS => SS.UploadPdf)
             .WithMany(S => S.TeacherSubject)
             .UsingEntity(j => j.ToTable("TeacherSubjectuploadpdf"))

        ;

            modelBuilder.Entity<TeacherSubject>()
             .HasMany(SS => SS.UploadVideo)
             .WithMany(S => S.TeacherSubject)
             .UsingEntity(j => j.ToTable("TeacherSubjectuploadVideo"))

        ;

            modelBuilder.Entity<TeacherSubject>()
              .HasMany(SS => SS.Quiz)
              .WithMany(S => S.TeacherSubject)
              .UsingEntity(j => j.ToTable("TeacherSubjectQuiz"))

         ;

            modelBuilder.Entity<Answer>()
         .HasMany(SS => SS.Question)
         .WithMany(S => S.Answers)
         .UsingEntity(j => j.ToTable("AnswersQuetions"));

            modelBuilder.Entity<Quiz>()
     .HasMany(SS => SS.Question)
     .WithMany(S => S.Quiz)
     .UsingEntity(j => j.ToTable("QuizQuetions"))
;
            modelBuilder.Entity<Quiz>()
.HasMany(SS => SS.TeacherSubject)
.WithMany(S => S.Quiz)
.UsingEntity(j => j.ToTable("QuizTeacherSubject"))
;
            modelBuilder.Entity<TeacherSubject>()
       .HasOne(ts => ts.Subject)
       .WithOne(s => s.TeacherSubject)  // Optional if you want a bidirectional relationship
       .HasForeignKey<TeacherSubject>(ts => ts.SubjectId);
        }

        public DbSet<Answer> Answer { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<SubscribeSubject> SubscribeSubject { get; set; }
        public DbSet<TeacherSubject> TeacherSubject { get; set; }
        public DbSet<UploadPdf> UploadPdf { get; set; }
        public DbSet<UploadVideo> UploadVideo { get; set; }
    }
}