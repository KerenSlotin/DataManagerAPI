using DataRetriever.DataStorage;

namespace DataRetriever.Factory
{
  public interface IDataStorageFactory
  {
    IDataStorage CreateDataStorage(DataStorageType sourceType);
    IEnumerable<DataStorageType> GetAllDataStorageTypes();
  }
}