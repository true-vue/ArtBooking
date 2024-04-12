using ArtBooking.Storage;
using ArtBooking.Storage.InMemory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArtBooking.Extensions.DependencyInjection;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddArtBookingStorageInMemory(this IServiceCollection services)
    {
        services.AddDbContext<ArtBookingDbContext>(options =>
            options.UseInMemoryDatabase("ArtBooking"));

        // Registed storage context to expose for upper layers storage .SaveChanges* methods;
        // Register as IArtBookingStorageContext (GPT solution)
        services.AddScoped<IArtBookingStorageContext, ArtBookingDbContext>(provider =>
            provider.GetService<ArtBookingDbContext>());

        // Registering OrganizationService from bussiness layer.
        services.AddScoped<IOrganizationRepository, OrganizationRepositoryInMemory>();

        // Registering LocationService from bussiness layer.
        services.AddScoped<ILocationRepository, LocationRepositoryInMemory>();

        return services;
    }
}