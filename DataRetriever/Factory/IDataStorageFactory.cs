using DataRetriever.DataStorage;

namespace DataRetriever.Factory
{
  /// <summary>
  /// Factory interface for creating data storage instances.
  /// </summary>
  public interface IDataStorageFactory
  {
    internal IDataStorage CreateDataStorage(DataStorageType sourceType);
    internal IEnumerable<DataStorageType> GetAllDataStorageTypes();
  }
}