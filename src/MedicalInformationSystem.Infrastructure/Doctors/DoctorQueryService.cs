using MedicalInformationSystem.Application.Doctors;
using MedicalInformationSystem.Domain.Entities;
using MedicalInformationSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedicalInformationSystem.Infrastructure.Doctors;

public sealed class DoctorQueryService(AppDbContext dbContext) : IDoctorQueryService
{
    public async Task<IReadOnlyList<DoctorListItemDto>> GetAllAsync(
        string? specialty,
        CancellationToken cancellationToken)
    {
        IQueryable<Doctor> query = dbContext.Doctors.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(specialty))
        {
            var normalizedSpecialty = specialty.Trim();
            query = query.Where(doctor => EF.Functions.ILike(
                doctor.Specialty,
                normalizedSpecialty));
        }

        return await query
            .OrderBy(doctor => doctor.Specialty)
            .ThenBy(doctor => doctor.LastName)
            .ThenBy(doctor => doctor.FirstName)
            .Select(doctor => new DoctorListItemDto(
                doctor.Id,
                doctor.FirstName,
                doctor.LastName,
                doctor.MiddleName,
                doctor.Specialty,
                doctor.LicenseNumber,
                doctor.Phone,
                doctor.Email,
                doctor.IsActive))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<string>> GetSpecialtiesAsync(
        CancellationToken cancellationToken)
    {
        return await dbContext.Doctors
            .AsNoTracking()
            .Select(doctor => doctor.Specialty)
            .Distinct()
            .OrderBy(specialty => specialty)
            .ToListAsync(cancellationToken);
    }
}
