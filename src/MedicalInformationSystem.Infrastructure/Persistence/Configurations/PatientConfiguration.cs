using MedicalInformationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalInformationSystem.Infrastructure.Persistence.Configurations;

public sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(patient => patient.Id);

        builder.Property(patient => patient.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(patient => patient.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(patient => patient.MiddleName)
            .HasMaxLength(100);

        builder.Property(patient => patient.Gender)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(patient => patient.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(patient => patient.Email)
            .HasMaxLength(254);

        builder.Property(patient => patient.MedicalPolicyNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(patient => patient.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasIndex(patient => patient.MedicalPolicyNumber)
            .IsUnique();
    }
}
