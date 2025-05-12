using DataRetriever.Models;

namespace DataRetriever.DataStorage
{
  public class CacheStorage : IDataStorage
  {
    public Task<DataItem> GetDataAsync(Guid id)
    {
      throw new NotImplementedException();
    }
  }
}