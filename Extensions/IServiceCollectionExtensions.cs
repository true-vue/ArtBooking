using ArtBooking.Application;
using ArtBooking.Storage;

namespace ArtBooking.Extensions.DependencyInjection;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddArtBookingAppLayers(this IServiceCollection services)
    {
        // Registering services for Dependency Injection container. This will allow us to use these dependencies in further class constructors.
        // Registering our in memory mockup
        services.AddSingleton(new ArtBookingDataMockup());

        // Registering OrganizationService from bussiness layer.
        services.AddScoped<IOrganizationService, OrganizationService>();

        // Registering OrganizationService from bussiness layer.
        services.AddScoped<IOrganizationRepository, OrganizationRepositoryMockup>();

        // Registering LocationService from bussiness layer.
        services.AddScoped<ILocationRepository, LocationRepositoryMockup>();


        return services;
    }
}

// into consideration when convection based registration can be done:
// using Scrutor;
// // Automatically register all services
// services.Scan(scan => scan
//     .FromAssemblyOf<Startup>()
//     .AddClasses(classes => classes.Where(type =>
//         type.Name.EndsWith("Service")))
//     .AsImplementedInterfaces()
//     .WithTransientLifetime());

// // Automatically register all repositories
// services.Scan(scan => scan
//     .FromAssemblyOf<Startup>()
//     .AddClasses(classes => classes.Where(type =>
//         type.Name.EndsWith("Repository")))
//     .AsImplementedInterfaces()
//     .WithTransientLifetime());
