using ElmnasaDomain.Entites.identity;
using ElmnasaInfrastructure.AppContext;
using ElmnasaInfrastructure.IdentityContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Elmnasa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ElmnasaContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Configure the database context for Identity
            builder.Services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppIdentityConnection")))
               ;

            builder.Services.AddDefaultIdentity<Account>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>() // Add roles
            .AddEntityFrameworkStores<AccountContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddIdentityCore<Student>(options =>
            {
            }).AddRoles<IdentityRole>() // Add roles
                .AddEntityFrameworkStores<AccountContext>()
                .AddSignInManager<SignInManager<Student>>()
                .AddDefaultTokenProviders();
            builder.Services.AddIdentityCore<Teacher>(options =>
            {
            }).AddRoles<IdentityRole>() // Add roles
          .AddEntityFrameworkStores<AccountContext>()
          .AddSignInManager<SignInManager<Teacher>>()
          .AddDefaultTokenProviders();

            builder.Services.AddIdentityCore<Admin>(options =>
            {
            }).AddRoles<IdentityRole>() // Add roles
         .AddEntityFrameworkStores<AccountContext>()
         .AddSignInManager<SignInManager<Admin>>()
         .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}