using MedicalInformationSystem.Application.Diseases;
using MedicalInformationSystem.Application.Doctors;
using MedicalInformationSystem.Application.Patients;
using MedicalInformationSystem.Infrastructure.Diseases;
using MedicalInformationSystem.Infrastructure.Doctors;
using MedicalInformationSystem.Infrastructure.Patients;
using MedicalInformationSystem.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MedicalInformationSystem.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<AppDbContext>("medicaldb");
        builder.Services.AddScoped<IDiseaseQueryService, DiseaseQueryService>();
        builder.Services.AddScoped<IDoctorQueryService, DoctorQueryService>();
        builder.Services.AddScoped<IPatientQueryService, PatientQueryService>();

        return builder;
    }
}
