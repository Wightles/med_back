namespace MedicalInformationSystem.Application.Diseases;

public interface IDiseaseQueryService
{
    Task<IReadOnlyList<DiseaseListItemDto>> GetAllAsync(
        CancellationToken cancellationToken);
}
