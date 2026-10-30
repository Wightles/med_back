using MedicalInformationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalInformationSystem.Infrastructure.Persistence.Configurations;

public sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(doctor => doctor.Id);

        builder.Property(doctor => doctor.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(doctor => doctor.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(doctor => doctor.MiddleName)
            .HasMaxLength(100);

        builder.Property(doctor => doctor.Specialty)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(doctor => doctor.LicenseNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(doctor => doctor.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(doctor => doctor.Email)
            .HasMaxLength(254);

        builder.Property(doctor => doctor.IsActive)
            .HasDefaultValue(true);

        builder.Property(doctor => doctor.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasIndex(doctor => doctor.LicenseNumber)
            .IsUnique();

        builder.HasIndex(doctor => doctor.Specialty);
    }
}
