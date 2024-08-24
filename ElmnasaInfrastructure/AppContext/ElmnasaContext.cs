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
                .HasOne(d => d.SubscribeSubject)
                .WithMany()
                .HasForeignKey(d => d.SubscribeSubjectId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Subject>()
                .HasOne(d => d.UploadPdf)
                .WithMany()
                .HasForeignKey(d => d.UploadPdfId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Subject>()
                .HasOne(m => m.UploadVideo)
                .WithMany()
                .HasForeignKey(m => m.UploadVideoId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Subject>()
              .HasOne(m => m.Quiz)
              .WithMany()
              .HasForeignKey(m => m.QuizId)
              .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Answer>()
            .HasOne(m => m.Question)
            .WithMany()
            .HasForeignKey(m => m.QuestionId)
            .OnDelete(DeleteBehavior.SetNull);
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