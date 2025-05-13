
namespace DataRetriever.DataStorage
{
  public interface IDataStorage
  {
    public DataStorageType StorageType { get; }
    Task<DataItem?> GetDataAsync(string id);
    Task SaveDataAsync(DataItem dataItem);
  }
}