using Xunit;
using MongoDBConnector;
using Testcontainers.MongoDb;


namespace MongoDBConnector.Tests;


public class MongoDBConnectorTests : IAsyncLifetime
{
    private readonly MongoDbContainer _mongoContainer;

    public MongoDBConnectorTests()
    {
        _mongoContainer = new MongoDbBuilder()
            .WithImage("mongo:7.0")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _mongoContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _mongoContainer.DisposeAsync();
    }

    [Fact]
    public async Task Ping_ShouldReturnTrue_WhenMongoIsRunning()
    {
        var connector = new MongoDBConnector(_mongoContainer.GetConnectionString());
        Assert.True(connector.Ping());
    }


    [Fact]
public async Task Ping_ShouldReturnFalse_WhenMongoIsNotAvailable()
{
    var connector = new MongoDBConnector("mongodb://localhost:12345");
    Assert.False(connector.Ping());
}
}


