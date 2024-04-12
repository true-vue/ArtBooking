using ArtBooking.Storage;
using ArtBooking.Storage.Mockup;
using Microsoft.Extensions.DependencyInjection;

namespace ArtBooking.Extensions.DependencyInjection;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddArtBookingStorageMockup(this IServiceCollection services)
    {
        // register mockup provider
        services.AddSingleton(new ArtBookingDataMockup());

        // Registed storage context to expose for upper layers storage .SaveChanges* methods;
        // In this case this is only empty implementation to satisfy interfaces - not need for saving changes in mockup;
        services.AddSingleton<IArtBookingStorageContext, ArtBookingStorageContext>();

        // Registering OrganizationService from bussiness layer.
        services.AddScoped<IOrganizationRepository, OrganizationRepositoryMockup>();

        // Registering LocationService from bussiness layer.
        services.AddScoped<ILocationRepository, LocationRepositoryMockup>();

        return services;
    }
}