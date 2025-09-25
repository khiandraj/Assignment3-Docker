using MongoDB.Driver;

namespace MongoDBConnector;

public class Class1
{

    private readonly MongoClient_client; 

    public MongoDBConnector (string connectionString){
        _client = new MongoClient(connectionString); 
    }
}
public bool Ping()
{
    try
    {
        var database = _client.GetDatabase("admin");
        var command = new BsonDocument("ping", 1);
        database.RunCommand<BsonDocument>(command);
        return true;
    }
    catch
    {
        return false;
    }
}