
using DataRetriever.Models;

public class DbStorage : IDataStorage
{
  public Task<DataItem> GetDataAsync(Guid id)
  {
    throw new NotImplementedException();
  }
}