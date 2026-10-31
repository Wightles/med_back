using Aspire.Hosting;

namespace MedicalInformationSystem.IntegrationTests;

public sealed class AppHostFixture : IAsyncLifetime
{
    private DistributedApplication? _application;

    public HttpClient ApiClient { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.MedicalInformationSystem_AppHost>(
                ["--environment=Development", "UseVolumes=false"]);

        _application = await builder.BuildAsync();
        await _application.StartAsync();

        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(2));
        await _application.ResourceNotifications.WaitForResourceHealthyAsync(
            "api",
            cancellationTokenSource.Token);

        ApiClient = _application.CreateHttpClient("api", "http");
    }

    public async Task DisposeAsync()
    {
        ApiClient.Dispose();

        if (_application is not null)
        {
            await _application.DisposeAsync();
        }
    }
}
