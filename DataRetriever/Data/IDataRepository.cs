using DataRetriever.Models;

namespace DataRetriever.Data
{
  public interface IDataRepository
  {
    Task<DataItem> GetDataAsync(Guid id);
  }
}