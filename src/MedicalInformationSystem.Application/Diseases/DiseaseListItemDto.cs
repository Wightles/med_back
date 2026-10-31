namespace MedicalInformationSystem.Application.Diseases;

public sealed record DiseaseListItemDto(
    Guid Id,
    string Name,
    string Icd10Code,
    string? Description,
    bool IsChronic);
