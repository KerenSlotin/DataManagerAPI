using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DataRetriever.DataStorage
{
  public class CacheStorage : IDataStorage
  {
    public DataStorageType StorageType => DataStorageType.Cache;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(10);
    public CacheStorage(IDistributedCache cache)
    {
      _cache = cache;
    }
    
    public async Task<DataItem?> GetDataAsync(string id)
    {
      var cachedItem = await _cache.GetStringAsync(id);
      return cachedItem != null 
                ? JsonConvert.DeserializeObject<DataItem>(cachedItem) 
                : null;
    }

    public async Task SaveDataAsync(DataItem dataItem)
    {
      await _cache.SetStringAsync(
            dataItem.Id, 
            JsonConvert.SerializeObject(dataItem),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry });
    }
  }
}