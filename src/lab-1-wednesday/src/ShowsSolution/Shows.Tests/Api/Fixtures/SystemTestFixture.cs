using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
namespace Shows.Tests.Api.Fixtures;

public class SystemTestFixture : IAsyncLifetime
{
    public IAlbaHost Host { get; private set; } = null!;
    public FakeTimeProvider FakeTime { get; private set; } = null!;
    public async Task InitializeAsync()
    {
        Host = await AlbaHost.For<Program>(config =>
        {
            FakeTime = new FakeTimeProvider(new DateTimeOffset(1969, 04, 20, 23, 59, 59, TimeSpan.FromHours(-4)));
            config.ConfigureTestServices(services =>
            {
             // services.AddSingleton<TimeProvider>(FakeTime);
            });
        });
    }

    public async Task DisposeAsync()
    {
        await Host.DisposeAsync();
    }
}

[CollectionDefinition("SystemTestFixture")]
public class SystemTestFixtureCollection : ICollectionFixture<SystemTestFixture>;