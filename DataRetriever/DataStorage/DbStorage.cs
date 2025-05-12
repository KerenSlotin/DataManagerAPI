using DataRetriever.Models;

namespace DataRetriever.DataStorage
{
  public class DbStorage : IDataStorage
  {
    public Task<DataItem> GetDataAsync(Guid id)
    {
      throw new NotImplementedException();
    }
  }
}