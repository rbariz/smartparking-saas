using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartParking.Domain.Entities;

namespace SmartParking.Infrastructure.Persistence.Configurations
{
    
    public sealed class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("drivers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.FullName)
                .HasColumnName("full_name")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Phone)
                .HasColumnName("phone")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(256);

            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();

            // 🔥 Nouveau champ
            builder.Property(x => x.LastLoginAtUtc)
                .HasColumnName("last_login_at_utc")
                .IsRequired(false);

            // 🔥 Nouveau champ
            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            // 🔍 Index utiles
            builder.HasIndex(x => x.Phone)
                .IsUnique();

            builder.HasIndex(x => x.Email);

            builder.HasIndex(x => x.CreatedAtUtc);
        }
    }
}
