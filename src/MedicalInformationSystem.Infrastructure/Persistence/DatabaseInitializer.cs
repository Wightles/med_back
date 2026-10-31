using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalInformationSystem.Infrastructure.Persistence;

public sealed class DatabaseInitializer(
    AppDbContext dbContext,
    ILogger<DatabaseInitializer> logger)
{
    public async Task InitializeAsync(
        bool seedDemoData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Applying database migrations");
            await dbContext.Database.MigrateAsync(cancellationToken);

            if (!seedDemoData)
            {
                return;
            }

            var containsData = await dbContext.Patients.AnyAsync(cancellationToken)
                || await dbContext.Doctors.AnyAsync(cancellationToken)
                || await dbContext.Diseases.AnyAsync(cancellationToken);

            if (containsData)
            {
                logger.LogInformation("Database already contains data, demo data will not be added");
                return;
            }

            logger.LogInformation("Adding demo medical data");
            await DemoData.SeedAsync(dbContext, cancellationToken);
            logger.LogInformation("Demo medical data added successfully");
        }
        catch (Exception exception)
        {
            logger.LogCritical(exception, "Database initialization failed");
            throw;
        }
    }
}
