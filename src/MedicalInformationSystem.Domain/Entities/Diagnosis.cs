namespace MedicalInformationSystem.Domain.Entities;

public sealed class Diagnosis
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DiseaseId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTimeOffset DiagnosedAt { get; set; }
    public string? Notes { get; set; }

    public required Patient Patient { get; set; }
    public required Disease Disease { get; set; }
    public required Doctor Doctor { get; set; }
}
