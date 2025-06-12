using NoteAppBackend.Repositories.Implementations;
using NoteAppBackend.Repositories.Interfaces;
using NoteAppBackend.Services.Implementations;
using NoteAppBackend.Services.Interfaces;

namespace NoteAppBackend.Extensions;

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