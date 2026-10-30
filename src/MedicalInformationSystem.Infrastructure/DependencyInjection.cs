using MedicalInformationSystem.Infrastructure.Persistence;
using Microsoft.Extensions.Hosting;

namespace MedicalInformationSystem.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<AppDbContext>("medicaldb");

        return builder;
    }
}
