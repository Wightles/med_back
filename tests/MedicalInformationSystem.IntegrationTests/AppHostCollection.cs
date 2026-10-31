namespace MedicalInformationSystem.IntegrationTests;

[CollectionDefinition(Name)]
public sealed class AppHostCollection : ICollectionFixture<AppHostFixture>
{
    public const string Name = "AppHost collection";
}
