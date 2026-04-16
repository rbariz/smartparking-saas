using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;

namespace SmartParking.Infrastructure.Persistence.Configurations
{
    public sealed class ParkingConfiguration : IEntityTypeConfiguration<Parking>
    {
        public void Configure(EntityTypeBuilder<Parking> builder)
        {
            builder.ToTable("parkings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.OperatorId)
                .HasColumnName("operator_id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.Address)
                .HasColumnName("address")
                .IsRequired();

            builder.Property(x => x.Latitude)
                .HasColumnName("latitude")
                .HasPrecision(9, 6)
                .IsRequired();

            builder.Property(x => x.Longitude)
                .HasColumnName("longitude")
                .HasPrecision(9, 6)
                .IsRequired();

            builder.Property(x => x.TotalCapacity)
                .HasColumnName("total_capacity")
                .IsRequired();

            builder.Property(x => x.AvailableCount)
                .HasColumnName("available_count")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => v == "open" ? ParkingStatus.Open : ParkingStatus.Closed)
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.HourlyRate)
                .HasColumnName("hourly_rate")
                .HasPrecision(12, 2)
                .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();

            builder.Property(x => x.UpdatedAtUtc)
                .HasColumnName("updated_at_utc")
                .IsRequired();

            builder.Ignore(x => x.IsOpen);

            builder.HasOne<Operator>()
                .WithMany()
                .HasForeignKey(x => x.OperatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.OperatorId)
                .HasDatabaseName("ix_parkings_operator");

            builder.HasIndex(x => new { x.Status, x.AvailableCount })
                .HasDatabaseName("ix_parkings_search");

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("ck_capacity_positive", "\"total_capacity\" > 0");
                t.HasCheckConstraint("ck_available_valid", "\"available_count\" >= 0 AND \"available_count\" <= \"total_capacity\"");
                t.HasCheckConstraint("ck_hourly_rate_valid", "\"hourly_rate\" >= 0");
                t.HasCheckConstraint("ck_currency_len", "char_length(\"currency\") = 3");
            });
        }
    }
}
