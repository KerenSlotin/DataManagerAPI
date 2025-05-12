
using DataRetriever.Models;

public class CacheStorage : IDataStorage
{
  public Task<DataItem> GetDataAsync(Guid id)
  {
    throw new NotImplementedException();
  }
}