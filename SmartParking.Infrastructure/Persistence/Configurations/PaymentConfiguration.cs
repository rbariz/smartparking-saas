using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;

namespace SmartParking.Infrastructure.Persistence.Configurations
{
    public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.BookingId)
                .HasColumnName("booking_id")
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasColumnName("amount")
                .HasPrecision(12, 2)
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.Status)
    .HasColumnName("status")
    .HasConversion(
        v => ToDatabase(v),
        v => FromDatabase(v))
    .IsRequired();

            builder.Property(x => x.Method)
                .HasColumnName("method")
                .IsRequired();

            builder.Property(x => x.ProviderReference)
                .HasColumnName("provider_reference");

            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();

            builder.Property(x => x.PaidAtUtc)
                .HasColumnName("paid_at_utc");

            builder.HasOne<Booking>()
                .WithMany()
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.BookingId)
                .HasDatabaseName("ix_payments_booking");

            builder.HasIndex(x => x.Status)
                .HasDatabaseName("ix_payments_status");

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("ck_amount_valid", "\"amount\" >= 0");
                t.HasCheckConstraint("ck_currency_len", "char_length(\"currency\") = 3");
            });
        }

        static string ToDatabase(PaymentStatus status) => status switch
        {
            PaymentStatus.Pending => "pending",
            PaymentStatus.Paid => "paid",
            PaymentStatus.Failed => "failed",
            _ => "pending"
        };

        static PaymentStatus FromDatabase(string value) => value switch
        {
            "pending" => PaymentStatus.Pending,
            "paid" => PaymentStatus.Paid,
            "failed" => PaymentStatus.Failed,
            _ => PaymentStatus.Pending
        };
    }
}
