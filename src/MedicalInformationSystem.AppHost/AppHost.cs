var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
    .WithDataVolume()
    .AddDatabase("medicaldb");

builder.AddProject<Projects.MedicalInformationSystem_Api>("api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
