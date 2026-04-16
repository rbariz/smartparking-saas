using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;

namespace SmartParking.Infrastructure.Persistence.Configurations
{
    public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("bookings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.DriverId)
                .HasColumnName("driver_id")
                .IsRequired();

            builder.Property(x => x.ParkingId)
                .HasColumnName("parking_id")
                .IsRequired();

            builder.Property(x => x.StartTimeUtc)
                .HasColumnName("start_time_utc")
                .IsRequired();

            builder.Property(x => x.EndTimeUtc)
                .HasColumnName("end_time_utc")
                .IsRequired();

            builder.Property(x => x.Status)
    .HasColumnName("status")
    .HasConversion(
        v => ToDatabase(v),
        v => FromDatabase(v))
    .IsRequired();

            builder.Property(x => x.ReservedPrice)
                .HasColumnName("reserved_price")
                .HasPrecision(12, 2)
                .IsRequired();

            builder.Property(x => x.PriceCurrency)
                .HasColumnName("price_currency")
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.ExpiresAtUtc)
                .HasColumnName("expires_at_utc")
                .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();

            builder.Property(x => x.UpdatedAtUtc)
                .HasColumnName("updated_at_utc")
                .IsRequired();

            builder.HasOne<Driver>()
                .WithMany()
                .HasForeignKey(x => x.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Parking>()
                .WithMany()
                .HasForeignKey(x => x.ParkingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.DriverId)
                .HasDatabaseName("ix_bookings_driver");

            builder.HasIndex(x => x.ParkingId)
                .HasDatabaseName("ix_bookings_parking");

            builder.HasIndex(x => x.Status)
                .HasDatabaseName("ix_bookings_status");

            builder.HasIndex(x => x.ExpiresAtUtc)
                .HasDatabaseName("ix_bookings_expiry");

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("ck_time_valid", "\"end_time_utc\" > \"start_time_utc\"");
                t.HasCheckConstraint("ck_price_valid", "\"reserved_price\" >= 0");
                t.HasCheckConstraint("ck_currency_len", "char_length(\"price_currency\") = 3");
            });
        }


        static string ToDatabase(BookingStatus status) => status switch
        {
            BookingStatus.PendingPayment => "pending_payment",
            BookingStatus.Confirmed => "confirmed",
            BookingStatus.Expired => "expired",
            BookingStatus.Cancelled => "cancelled",
            _ => "pending_payment"
        };

        static BookingStatus FromDatabase(string value) => value switch
        {
            "pending_payment" => BookingStatus.PendingPayment,
            "confirmed" => BookingStatus.Confirmed,
            "expired" => BookingStatus.Expired,
            "cancelled" => BookingStatus.Cancelled,
            _ => BookingStatus.PendingPayment
        };
    }
}
