using Microsoft.Azure.Management.CosmosDB.Fluent;
using MongoDB.Driver;

namespace DataRetriever.Repository
{
  public class MongoDbRepository : IDataRepository
  {
    private readonly IMongoCollection<DataItem> _collection;

    public MongoDbRepository(IMongoDbSettings settings)
    {
      var client = new MongoClient(settings.ConnectionString);
      var database = client.GetDatabase(settings.DatabaseName);
      _collection = database.GetCollection<DataItem>(settings.CollectionName);
    }

    public async Task<DataItem?> GetDataAsync(string id)
    {
      var filter = Builders<DataItem>.Filter.Eq(data => data.Id, id);
      return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(DataItem dataItem)
    {
      await _collection.InsertOneAsync(dataItem);
    }

    public async Task<bool> UpdateAsync(DataItem dataItem)
    {
      var filter = Builders<DataItem>.Filter.Eq(x => x.Id, dataItem.Id);
      var updateResult = await _collection.ReplaceOneAsync(filter, dataItem);
      return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
  }
}