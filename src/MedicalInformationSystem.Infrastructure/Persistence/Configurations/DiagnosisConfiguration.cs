using MedicalInformationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalInformationSystem.Infrastructure.Persistence.Configurations;

public sealed class DiagnosisConfiguration : IEntityTypeConfiguration<Diagnosis>
{
    public void Configure(EntityTypeBuilder<Diagnosis> builder)
    {
        builder.HasKey(diagnosis => diagnosis.Id);

        builder.Property(diagnosis => diagnosis.DiagnosedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(diagnosis => diagnosis.Notes)
            .HasMaxLength(2000);

        builder.HasOne(diagnosis => diagnosis.Patient)
            .WithMany(patient => patient.Diagnoses)
            .HasForeignKey(diagnosis => diagnosis.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(diagnosis => diagnosis.Doctor)
            .WithMany(doctor => doctor.Diagnoses)
            .HasForeignKey(diagnosis => diagnosis.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(diagnosis => diagnosis.Disease)
            .WithMany(disease => disease.Diagnoses)
            .HasForeignKey(diagnosis => diagnosis.DiseaseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
