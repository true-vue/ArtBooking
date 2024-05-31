using ArtBooking.Model;
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

    public static async void UseArtBookingInMemoryMockup(this IServiceProvider provider)
    {
        using (var scope = provider.CreateScope())
        {
            var scopedProvider = scope.ServiceProvider;

            var dbContext = scopedProvider.GetRequiredService<IArtBookingStorageContext>();
            var organizationRepository = scopedProvider.GetRequiredService<IOrganizationRepository>();
            var locationRepository = scopedProvider.GetService<ILocationRepository>();
            var bagatela = new Organization()
            {
                OrganizationName = "Teatr Bagatela",
                // Karmelicka 6, 31-128 Kraków
                Address = new Address()
                {
                    Street = "Karmelicka",
                    AddressNumber = "6",
                    Town = "Kraków",
                    PostalCode = "31-128"
                },
            };
            var kijow = new Organization()
            {
                OrganizationName = "Kino Kijów",
                // Aleja Zygmunta Krasińskiego 34, 30-101 Kraków
                Address = new Address()
                {
                    Street = "Aleja Zygmunta Krasińskiego",
                    AddressNumber = "34",
                    Town = "Kraków",
                    PostalCode = "30-101"
                }
            };
            var multikino = new Organization()
            {
                OrganizationName = "Multikino Kraków",
                // Dobrego Pasterza 128, 31-416 Kraków
                Address = new Address()
                {
                    Street = "Dobrego Pasterza",
                    AddressNumber = "128",
                    Town = "Kraków",
                    PostalCode = "31-416"
                }
            };
            await organizationRepository.AddAsync(bagatela);
            await organizationRepository.AddAsync(kijow);
            await organizationRepository.AddAsync(multikino);

            dbContext.SaveChanges();

            await locationRepository.AddAsync(new Location()
            {
                OrganizationId = bagatela.OrganizationId,
                LocationName = "Duża scena",
                Description = "Duża scena mnieszcząca się w siedzibie głównej teatru.",
                Address = bagatela.Address
            });
            await locationRepository.AddAsync(new Location()
            {
                OrganizationId = multikino.OrganizationId,
                LocationName = "Sala 1",
                Description = "Liczba miejsc: 473 (w tym 33 fotele VIP).",
                Address = multikino.Address
            });
            await locationRepository.AddAsync(new Location()
            {
                OrganizationId = multikino.OrganizationId,
                LocationName = "Sala 2",
                Description = "Liczba miejsc: 192 (w tym 24 fotele VIP).",
                Address = multikino.Address
            });
            await locationRepository.AddAsync(new Location()
            {
                OrganizationId = multikino.OrganizationId,
                LocationName = "Sala 3",
                Description = "Liczba miejsc: 124 (w tym 12 foteli VIP).",
                Address = multikino.Address
            });
            await locationRepository.AddAsync(new Location()
            {
                OrganizationId = multikino.OrganizationId,
                LocationName = "Sala 4",
                Description = "Liczba miejsc: 185 (w tym 12 foteli VIP).",
                Address = multikino.Address
            });
            await locationRepository.AddAsync(new Location()
            {
                OrganizationId = multikino.OrganizationId,
                LocationName = "Sala 5",
                Description = "Liczba miejsc: 330 (w tym 24 fotele VIP).",
                Address = multikino.Address
            });

            dbContext.SaveChanges();

        }
    }
}