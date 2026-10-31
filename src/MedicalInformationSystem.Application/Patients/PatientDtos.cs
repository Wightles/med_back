using MedicalInformationSystem.Domain.Enums;

namespace MedicalInformationSystem.Application.Patients;

public sealed record PatientListItemDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    string Phone,
    string MedicalPolicyNumber);

public sealed record PatientDetailsDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    Gender Gender,
    string Phone,
    string? Email,
    string MedicalPolicyNumber,
    DateTimeOffset CreatedAt,
    IReadOnlyList<PatientDiagnosisDto> Diagnoses);

public sealed record PatientDiagnosisDto(
    Guid Id,
    DateTimeOffset DiagnosedAt,
    string? Notes,
    Guid DiseaseId,
    string DiseaseName,
    string Icd10Code,
    Guid DoctorId,
    string DoctorFirstName,
    string DoctorLastName,
    string? DoctorMiddleName,
    string DoctorSpecialty);
