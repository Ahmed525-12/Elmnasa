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

            modelBuilder.Entity<Subject>()
                .HasMany(SS => SS.SubscribeSubject)
                .WithMany(S => S.Subject)
                .UsingEntity(j => j.ToTable("StudentSubscribeSubject"))

           ;

            modelBuilder.Entity<Subject>()
             .HasMany(SS => SS.UploadPdf)
             .WithMany(S => S.Subject)
             .UsingEntity(j => j.ToTable("subjectuploadpdf"))

        ;

            modelBuilder.Entity<Subject>()
             .HasMany(SS => SS.UploadVideo)
             .WithMany(S => S.Subject)
             .UsingEntity(j => j.ToTable("subjectuploadVideo"))

        ;

            modelBuilder.Entity<Subject>()
              .HasMany(SS => SS.Quiz)
              .WithMany(S => S.Subject)
              .UsingEntity(j => j.ToTable("subjectQuiz"))

         ;

            modelBuilder.Entity<Answer>()
         .HasMany(SS => SS.Question)
         .WithMany(S => S.Answers)
         .UsingEntity(j => j.ToTable("AnswersQuetions"))

    ;
        }

        public DbSet<Answer> Answer { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<SubscribeSubject> SubscribeSubject { get; set; }
        public DbSet<UploadPdf> UploadPdf { get; set; }
        public DbSet<UploadVideo> UploadVideo { get; set; }
    }
}