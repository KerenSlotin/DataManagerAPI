

using DataRetriever.Models;

public interface IDataStorage
{
  Task<DataItem> GetDataAsync(Guid id);
}