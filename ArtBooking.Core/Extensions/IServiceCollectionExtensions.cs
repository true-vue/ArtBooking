using ArtBooking.Application;
using Microsoft.Extensions.DependencyInjection;

namespace ArtBooking.Extensions.DependencyInjection;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddArtBookingCore(this IServiceCollection services)
    {
        // Registering OrganizationService from bussiness layer.
        services.AddScoped<IOrganizationService, OrganizationService>();

        return services;
    }
}