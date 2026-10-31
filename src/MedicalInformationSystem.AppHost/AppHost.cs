using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

if (builder.Configuration.GetValue("UseVolumes", true))
{
    postgres.WithDataVolume();
}

var database = postgres.AddDatabase("medicaldb");

builder.AddProject<Projects.MedicalInformationSystem_Api>("api")
    .WithReference(database)
    .WaitFor(database)
    .WithHttpHealthCheck("/health");

builder.Build().Run();
