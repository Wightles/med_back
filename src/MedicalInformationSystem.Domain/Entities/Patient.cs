using MedicalInformationSystem.Domain.Enums;

namespace MedicalInformationSystem.Domain.Entities;

public sealed class Patient
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public required string Phone { get; set; }
    public string? Email { get; set; }
    public required string MedicalPolicyNumber { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<Diagnosis> Diagnoses { get; } = [];
}
