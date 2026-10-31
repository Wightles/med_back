namespace MedicalInformationSystem.Application.Patients;

public interface IPatientQueryService
{
    Task<IReadOnlyList<PatientListItemDto>> GetAllAsync(CancellationToken cancellationToken);

    Task<PatientDetailsDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
