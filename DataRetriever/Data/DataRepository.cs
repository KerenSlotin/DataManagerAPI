using DataRetriever.Models;

namespace DataRetriever.Data
{
  public class DataRepository : IDataRepository
  {
    public Task<DataItem> GetDataAsync(Guid id)
    {
      throw new NotImplementedException();
    }
  }
}