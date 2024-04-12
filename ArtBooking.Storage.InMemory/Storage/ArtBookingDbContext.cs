using ArtBooking.Model;
using Microsoft.EntityFrameworkCore;

public class ArtBookingDbContext : DbContext, IArtBookingStorageContext
{

    public ArtBookingDbContext(DbContextOptions<ArtBookingDbContext> options) : base(options)
    {
    }

    public DbSet<Organization> Organizations { get; set; }

    public DbSet<Location> Locations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the Organization entity
        modelBuilder.Entity<Organization>(builder =>
        {
            // Map Address properties directly inside the Organizations table
            builder.OwnsOne(o => o.Address, am =>
            {
                // Specify the column names if desired, or let EF use default naming conventions
                am.Property(a => a.Street);
                am.Property(a => a.Town);
                am.Property(a => a.AddressNumber);
                am.Property(a => a.PostalCode);
                am.Property(a => a.Country);
            });

            // Other configurations for Organization
            builder.HasKey(o => o.OrganizationId);
        });

        // Define the Location entity
        modelBuilder.Entity<Location>(builder =>
        {
            // Map Address properties directly inside the Location table
            builder.OwnsOne(l => l.Address, am =>
            {
                // Specify the column names if desired, or let EF use default naming conventions
                am.Property(a => a.Street);
                am.Property(a => a.Town);
                am.Property(a => a.AddressNumber);
                am.Property(a => a.PostalCode);
                am.Property(a => a.Country);
            });

            // Other configurations for Organization
            builder.HasKey(l => l.LocationId);
        });
    }
}