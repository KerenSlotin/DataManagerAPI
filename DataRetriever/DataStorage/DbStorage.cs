using DataRetriever.Models;

namespace DataRetriever.DataStorage
{
  public class DbStorage : IDataStorage
  {
    public DataStorageType StorageType => DataStorageType.Database;
    public Task<DataItem> GetDataAsync(string id)
    {
      return null;
    }

    public Task<bool> SaveDataAsync(DataItem dataItem)
    {
      throw new NotImplementedException();
    }
  }
}