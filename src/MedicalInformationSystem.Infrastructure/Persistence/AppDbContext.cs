using MedicalInformationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalInformationSystem.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Disease> Diseases => Set<Disease>();
    public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
