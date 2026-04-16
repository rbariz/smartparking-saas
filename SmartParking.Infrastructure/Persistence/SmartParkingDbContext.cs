using Microsoft.EntityFrameworkCore;
using SmartParking.Domain.Entities;

namespace SmartParking.Infrastructure.Persistence
{
    public sealed class SmartParkingDbContext : DbContext
    {
        public SmartParkingDbContext(DbContextOptions<SmartParkingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Operator> Operators => Set<Operator>();
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Parking> Parkings => Set<Parking>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartParkingDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }


}
