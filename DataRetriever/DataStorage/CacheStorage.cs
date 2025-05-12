using DataRetriever.Models;

namespace DataRetriever.DataStorage
{
  public class CacheStorage : IDataStorage
  {
    public DataStorageType StorageType => DataStorageType.Cache;
    public async Task<DataItem> GetDataAsync(string id)
    {
      return new DataItem
      {
        Id = id,
        Value = "Cached Data",
        CreatedAt = DateTime.UtcNow
      };
    }
    public Task<bool> SaveDataAsync(DataItem dataItem)
    {
      return Task.FromResult(true);
    }
  }
}