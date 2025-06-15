using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using NoteAppBackend.Data;
using NoteAppBackend.Extensions;

namespace NoteAppBackend
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Create the web application builder
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // === SERVICE REGISTRATION ===

            // Authentication & Authorization services
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));
            builder.Services.AddAuthorization();

            // Database context registration with PostgresSQL
            builder.Services.AddDbContext<AppDbContext>(opts =>
                opts.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            // Custom application services (dependency injection)
            builder.Services.AddApplicationServices();

            // Cross-Origin Resource Sharing (CORS) policy for Vue.js frontend
            builder.Services.AddCors(o =>
                o.AddPolicy("AllowVue", p =>
                    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            // API documentation and controller services
            builder.Services.AddOpenApi();
            builder.Services.AddControllers();

            // Build the application
            WebApplication app = builder.Build();

            // === MIDDLEWARE PIPELINE CONFIGURATION ===

            // Enable OpenAPI/Swagger in development environment only
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // Security middleware - redirect HTTP to HTTPS
            app.UseHttpsRedirection();

            // Enable authorization middleware
            app.UseAuthorization();

            // Apply CORS policy for cross-origin requests
            app.UseCors("AllowVue");

            // Map controller endpoints
            app.MapControllers();

            // Start the application
            app.Run();
        }
    }
}