namespace MedicalInformationSystem.Application.Doctors;

public sealed record DoctorListItemDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string Specialty,
    string LicenseNumber,
    string Phone,
    string? Email,
    bool IsActive);
