namespace ArtBooking.Storage.Mockup;

using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ArtBooking.Model;

public class ArtBookingStorageContext : IArtBookingStorageContext
{
    // just an empty implementation to satify upper layers...

    public int SaveChanges() { return 1; }

    public int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return 1;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(1);
    }

    public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(1);
    }
}