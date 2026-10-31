using MedicalInformationSystem.Domain.Entities;
using MedicalInformationSystem.Domain.Enums;

namespace MedicalInformationSystem.Infrastructure.Persistence;

internal static class DemoData
{
    public static async Task SeedAsync(
        AppDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var createdAt = new DateTimeOffset(2026, 10, 30, 2, 0, 0, TimeSpan.Zero);

        var cardiologist = new Doctor
        {
            Id = Guid.Parse("8c810e47-538d-4adb-a6e0-88d893ad42e4"),
            FirstName = "Елена",
            LastName = "Соколова",
            MiddleName = "Викторовна",
            Specialty = "Кардиолог",
            LicenseNumber = "NSK-001-2020",
            Phone = "+7-913-100-10-10",
            Email = "e.sokolova@example.com",
            IsActive = true,
            CreatedAt = createdAt
        };

        var therapist = new Doctor
        {
            Id = Guid.Parse("294ca5f3-c81c-4860-b6d4-8cd1021f8502"),
            FirstName = "Михаил",
            LastName = "Орлов",
            MiddleName = "Андреевич",
            Specialty = "Терапевт",
            LicenseNumber = "NSK-002-2019",
            Phone = "+7-913-200-20-20",
            Email = "m.orlov@example.com",
            IsActive = true,
            CreatedAt = createdAt
        };

        var hypertension = new Disease
        {
            Id = Guid.Parse("4678a8db-9683-43a8-a082-3d24f6ea3081"),
            Name = "Эссенциальная (первичная) гипертензия",
            Icd10Code = "I10",
            Description = "Стойкое повышение артериального давления.",
            IsChronic = true
        };

        var respiratoryInfection = new Disease
        {
            Id = Guid.Parse("b90d8a67-ae2a-42dd-a9a4-a60bd5563a35"),
            Name = "Острая инфекция верхних дыхательных путей",
            Icd10Code = "J06.9",
            Description = "Острая инфекция верхних дыхательных путей неуточненная.",
            IsChronic = false
        };

        var firstPatient = new Patient
        {
            Id = Guid.Parse("65da26b0-3a12-4f8e-8509-9c7d94591ab9"),
            FirstName = "Анна",
            LastName = "Петрова",
            MiddleName = "Сергеевна",
            DateOfBirth = new DateOnly(1985, 4, 12),
            Gender = Gender.Female,
            Phone = "+7-913-300-30-30",
            Email = "a.petrova@example.com",
            MedicalPolicyNumber = "5400123456789012",
            CreatedAt = createdAt
        };

        var secondPatient = new Patient
        {
            Id = Guid.Parse("437b5414-bebc-4858-bcc5-cba1b78bf644"),
            FirstName = "Алексей",
            LastName = "Смирнов",
            MiddleName = "Игоревич",
            DateOfBirth = new DateOnly(1992, 11, 3),
            Gender = Gender.Male,
            Phone = "+7-913-400-40-40",
            Email = "a.smirnov@example.com",
            MedicalPolicyNumber = "5400987654321098",
            CreatedAt = createdAt
        };

        var diagnoses = new[]
        {
            new Diagnosis
            {
                Id = Guid.Parse("b2e19516-b5ed-4076-b2a1-61342714031b"),
                PatientId = firstPatient.Id,
                DiseaseId = hypertension.Id,
                DoctorId = cardiologist.Id,
                DiagnosedAt = new DateTimeOffset(2026, 9, 15, 3, 30, 0, TimeSpan.Zero),
                Notes = "Рекомендован ежедневный контроль артериального давления.",
                Patient = firstPatient,
                Disease = hypertension,
                Doctor = cardiologist
            },
            new Diagnosis
            {
                Id = Guid.Parse("57ce6c68-5e45-4670-8a40-783a22757a9e"),
                PatientId = secondPatient.Id,
                DiseaseId = respiratoryInfection.Id,
                DoctorId = therapist.Id,
                DiagnosedAt = new DateTimeOffset(2026, 10, 28, 8, 0, 0, TimeSpan.Zero),
                Notes = "Назначено симптоматическое лечение.",
                Patient = secondPatient,
                Disease = respiratoryInfection,
                Doctor = therapist
            }
        };

        dbContext.AddRange(
            cardiologist,
            therapist,
            hypertension,
            respiratoryInfection,
            firstPatient,
            secondPatient);
        dbContext.Diagnoses.AddRange(diagnoses);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
