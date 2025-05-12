using System.Text.Json;
using DataRetriever.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace DataRetriever.DataStorage
{
  public class CacheStorage : IDataStorage
  {
    public DataStorageType StorageType => DataStorageType.Cache;
    private readonly IDistributedCache _cache;

    public CacheStorage(IDistributedCache cache)
    {
      _cache = cache;
    }
    
    public async Task<DataItem?> GetDataAsync(string id)
    {
      var cachedData = await _cache.GetStringAsync(id);
      return cachedData != null 
                ? JsonSerializer.Deserialize<DataItem>(cachedData) 
                : null;
      // return new DataItem
      // {
      //   Id = id,
      //   Value = "Cached Data",
      //   CreatedAt = DateTime.UtcNow
      // };
    }
    public Task<bool> SaveDataAsync(DataItem dataItem)
    {
      return Task.FromResult(true);
    }
  }
}