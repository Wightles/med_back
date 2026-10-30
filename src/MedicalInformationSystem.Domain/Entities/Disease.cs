namespace MedicalInformationSystem.Domain.Entities;

public sealed class Disease
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Icd10Code { get; set; }
    public string? Description { get; set; }
    public bool IsChronic { get; set; }

    public ICollection<Diagnosis> Diagnoses { get; } = [];
}
