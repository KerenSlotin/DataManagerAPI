using DataRetriever.Models;

namespace DataRetriever.DataStorage
{
  public interface IDataStorage
  {
    public DataStorageType StorageType { get; }
    Task<DataItem?> GetDataAsync(string id);
    Task<bool> SaveDataAsync(DataItem dataItem);
  }
}