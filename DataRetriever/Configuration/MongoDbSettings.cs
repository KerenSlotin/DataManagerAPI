
internal interface IMongoDbSettings
{
  string ConnectionString { get; set; }
  string DatabaseName { get; set; }
  string CollectionName { get; set; }
}

internal class MongoDbSettings : IMongoDbSettings
{
  public required string ConnectionString { get; set; }
  public required string DatabaseName { get; set; }
  public required string CollectionName { get; set; }
}