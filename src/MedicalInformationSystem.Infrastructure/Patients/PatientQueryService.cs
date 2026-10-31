using MedicalInformationSystem.Application.Patients;
using MedicalInformationSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedicalInformationSystem.Infrastructure.Patients;

public sealed class PatientQueryService(AppDbContext dbContext) : IPatientQueryService
{
    public async Task<IReadOnlyList<PatientListItemDto>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await dbContext.Patients
            .AsNoTracking()
            .OrderBy(patient => patient.LastName)
            .ThenBy(patient => patient.FirstName)
            .Select(patient => new PatientListItemDto(
                patient.Id,
                patient.FirstName,
                patient.LastName,
                patient.MiddleName,
                patient.DateOfBirth,
                patient.Phone,
                patient.MedicalPolicyNumber))
            .ToListAsync(cancellationToken);
    }

    public Task<PatientDetailsDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return dbContext.Patients
            .AsNoTracking()
            .Where(patient => patient.Id == id)
            .Select(patient => new PatientDetailsDto(
                patient.Id,
                patient.FirstName,
                patient.LastName,
                patient.MiddleName,
                patient.DateOfBirth,
                patient.Gender,
                patient.Phone,
                patient.Email,
                patient.MedicalPolicyNumber,
                patient.CreatedAt,
                patient.Diagnoses
                    .OrderByDescending(diagnosis => diagnosis.DiagnosedAt)
                    .Select(diagnosis => new PatientDiagnosisDto(
                        diagnosis.Id,
                        diagnosis.DiagnosedAt,
                        diagnosis.Notes,
                        diagnosis.DiseaseId,
                        diagnosis.Disease.Name,
                        diagnosis.Disease.Icd10Code,
                        diagnosis.DoctorId,
                        diagnosis.Doctor.FirstName,
                        diagnosis.Doctor.LastName,
                        diagnosis.Doctor.MiddleName,
                        diagnosis.Doctor.Specialty))
                    .ToList()))
            .SingleOrDefaultAsync(cancellationToken);
    }
}
