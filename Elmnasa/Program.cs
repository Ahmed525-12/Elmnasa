using ElmnasaDomain.DTOs.EmailDTO;
using ElmnasaDomain.Entites.identity;
using ElmnasaInfrastructure.AppContext;
using ElmnasaInfrastructure.IdentityContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ElmnasaApp.ServicesExtension;

namespace Elmnasa
{
    public class Program
    {
        public static async Task Main(string[] args)
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
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            // Configure the database context for Identity
            builder.Services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppIdentityConnection")))
               ;

            builder.Services.AddDefaultIdentity<Account>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddRoles<IdentityRole>() // Add roles
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AccountContext>()

            ;

            builder.Services.AddIdentityCore<Student>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddRoles<IdentityRole>() // Add roles
            .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AccountContext>()
                .AddSignInManager<SignInManager<Student>>()
                ;
            builder.Services.AddIdentityCore<Teacher>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddRoles<IdentityRole>() // Add roles
            .AddDefaultTokenProviders()
          .AddEntityFrameworkStores<AccountContext>()
          .AddSignInManager<SignInManager<Teacher>>()

          ;

            builder.Services.AddIdentityCore<Admin>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddRoles<IdentityRole>() // Add roles
            .AddDefaultTokenProviders()
         .AddEntityFrameworkStores<AccountContext>()
         .AddSignInManager<SignInManager<Admin>>()
         ;
            builder.Services.AddAplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Student", "Teacher", "Admin" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            app.Run();
        }
    }
}