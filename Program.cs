using Microsoft.EntityFrameworkCore;
using BugsTrackingSystem.Models;
using BugsTrackingSystem.DAL;
using BugsTrackingSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BugsTrackingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BugsResolvingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Development"));
            });

            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("App_Cors_Policy", builder =>
                {
                    builder
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IBugsService, BugsService>();
            builder.Services.AddScoped<IMessagesService, MessagesService>();
            builder.Services.AddScoped<IProjectsService, ProjectsService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:JwtSecret").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddAuthorization(options => {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Roles", "Admin"));
                options.AddPolicy("User", policy => policy.RequireClaim("Roles", "User","Admin"));
                options.AddPolicy("Developer", policy => policy.RequireClaim("Roles", "Developer","Admin","Manager"));
                options.AddPolicy("Manager", policy => policy.RequireClaim("Roles", "Manager","Admin"));

            });

            var app = builder.Build();

            app.UseCors("App_Cors_Policy"); app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix=string.Empty;
                options.DocumentTitle = "My Swagger";
            });
            app.MapControllers();
            app.Run();


        }
    }
}