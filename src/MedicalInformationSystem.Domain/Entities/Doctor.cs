namespace MedicalInformationSystem.Domain.Entities;

public sealed class Doctor
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required string Specialty { get; set; }
    public required string LicenseNumber { get; set; }
    public required string Phone { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<Diagnosis> Diagnoses { get; } = [];
}
