using DataRetriever.Models;

namespace DataRetriever.DataStorage
{
  public interface IDataStorage
  {
    Task<DataItem> GetDataAsync(Guid id);
  }
}