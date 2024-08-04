using ElmnasaDomain.Entites.identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaInfrastructure.IdentityContext
{
    public class AccountContext : IdentityDbContext<Account, IdentityRole, string>
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Account>(entity => { entity.ToTable("Accounts"); });
            builder.Entity<Student>(entity => { entity.ToTable("Student"); });
            builder.Entity<Teacher>(entity => { entity.ToTable("Teacher"); });
            builder.Entity<Admin>(entity => { entity.ToTable("Admin"); });
        }

        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }
    }
}