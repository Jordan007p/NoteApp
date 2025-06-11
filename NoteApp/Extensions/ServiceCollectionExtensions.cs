using NoteApp.Repositories.Implementations;
using NoteApp.Repositories.Interfaces;
using NoteApp.Services.Implementations;
using NoteApp.Services.Interfaces;

namespace NoteApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register repositories
        services.AddScoped<INoteRepository, NoteRepository>();

        // Register services
        services.AddScoped<INoteService, NoteService>();

        return services;
    }
}