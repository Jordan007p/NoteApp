using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using NoteAppBackend.Data;

namespace NoteAppBackend;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));
        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddCors(o => o.AddPolicy("AllowVue", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        builder.Services.AddOpenApi();
        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors("AllowVue");

        // Keep only the controller mapping
        app.MapControllers();

        app.Run();
    }
}