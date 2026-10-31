namespace MedicalInformationSystem.Application.Doctors;

public interface IDoctorQueryService
{
    Task<IReadOnlyList<DoctorListItemDto>> GetAllAsync(
        string? specialty,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<string>> GetSpecialtiesAsync(CancellationToken cancellationToken);
}
