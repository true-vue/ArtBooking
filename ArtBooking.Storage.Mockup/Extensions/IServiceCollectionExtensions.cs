using ArtBooking.Application;
using ArtBooking.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace ArtBooking.Extensions.DependencyInjection;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddArtBookingStorageMockup(this IServiceCollection services)
    {
        // register mockup provider
        services.AddSingleton(new ArtBookingDataMockup());

        // Registering OrganizationService from bussiness layer.
        services.AddScoped<IOrganizationRepository, OrganizationRepositoryMockup>();

        // Registering LocationService from bussiness layer.
        services.AddScoped<ILocationRepository, LocationRepositoryMockup>();

        return services;
    }
}