using Xunit;
using MongoDBConnector;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;


namespace MongoDBConnector.Tests;


public class MongoDBConnectorTests : IAsyncLifetime
{
    private readonly TestcontainersContainer _mongoContainer;
    private string _connectionString = string.Empty;

    public MongoDBConnectorTests()
    {
        _mongoContainer = new TestcontainersBuilder<TestcontainersContainer>()
            .WithImage("mongo:6.0")
            .WithPortBinding(27017, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(27017))
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _mongoContainer.StartAsync();
        var port = _mongoContainer.GetMappedPublicPort(27017);
        _connectionString = $"mongodb://localhost:{port}";
    }

    public async Task DisposeAsync()
    {
        await _mongoContainer.DisposeAsync();
    }

    [Fact]
    public void Ping_ShouldReturnTrue_WhenMongoIsRunning()
    {
        var connector = new MongoDBConnector(_connectionString);
        Assert.True(connector.Ping());
    }


    [Fact]
public void Ping_ShouldReturnFalse_WhenMongoIsNotAvailable()
{
    var connector = new MongoDBConnector("mongodb://localhost:12345");
    Assert.False(connector.Ping());
}
}


