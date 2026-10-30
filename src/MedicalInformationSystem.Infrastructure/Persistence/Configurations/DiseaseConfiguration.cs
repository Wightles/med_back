using MedicalInformationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalInformationSystem.Infrastructure.Persistence.Configurations;

public sealed class DiseaseConfiguration : IEntityTypeConfiguration<Disease>
{
    public void Configure(EntityTypeBuilder<Disease> builder)
    {
        builder.HasKey(disease => disease.Id);

        builder.Property(disease => disease.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(disease => disease.Icd10Code)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(disease => disease.Description)
            .HasMaxLength(2000);

        builder.HasIndex(disease => disease.Icd10Code)
            .IsUnique();
    }
}
