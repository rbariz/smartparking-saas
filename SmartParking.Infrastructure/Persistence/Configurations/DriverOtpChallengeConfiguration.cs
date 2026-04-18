using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartParking.Domain.Entities;

namespace SmartParking.Infrastructure.Persistence.Configurations
{
    public sealed class DriverOtpChallengeConfiguration : IEntityTypeConfiguration<DriverOtpChallenge>
    {
        public void Configure(EntityTypeBuilder<DriverOtpChallenge> builder)
        {
            builder.ToTable("DriverOtpChallenges");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Contact)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Channel)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Purpose)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.CodeHash)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();

            builder.Property(x => x.ExpiresAtUtc)
                .IsRequired();

            builder.Property(x => x.ConsumedAtUtc)
                .IsRequired(false);

            builder.Property(x => x.LastSentAtUtc)
                .IsRequired();

            builder.Property(x => x.AttemptCount)
                .IsRequired();

            builder.Property(x => x.MaxAttempts)
                .IsRequired();

            builder.Property(x => x.IsBlocked)
                .IsRequired();

            // 🔍 Index utiles
            builder.HasIndex(x => x.Contact);
            builder.HasIndex(x => new { x.Contact, x.Purpose });
            builder.HasIndex(x => x.CreatedAtUtc);

            builder.HasIndex(x => new { x.Contact, x.Purpose, x.CreatedAtUtc });
        }
    }
}
