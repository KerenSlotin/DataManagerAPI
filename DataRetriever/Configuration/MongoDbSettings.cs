
public interface IMongoDbSettings
{
  string ConnectionString { get; set; }
  string DatabaseName { get; set; }
  string CollectionName { get; set; }
}

public class MongoDbSettings : IMongoDbSettings
{
  public string ConnectionString { get; set; }
  public string DatabaseName { get; set; }
  public string CollectionName { get; set; }
}