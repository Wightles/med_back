using MedicalInformationSystem.Application.Diseases;
using MedicalInformationSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedicalInformationSystem.Infrastructure.Diseases;

public sealed class DiseaseQueryService(AppDbContext dbContext) : IDiseaseQueryService
{
    public async Task<IReadOnlyList<DiseaseListItemDto>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await dbContext.Diseases
            .AsNoTracking()
            .OrderBy(disease => disease.Icd10Code)
            .Select(disease => new DiseaseListItemDto(
                disease.Id,
                disease.Name,
                disease.Icd10Code,
                disease.Description,
                disease.IsChronic))
            .ToListAsync(cancellationToken);
    }
}
