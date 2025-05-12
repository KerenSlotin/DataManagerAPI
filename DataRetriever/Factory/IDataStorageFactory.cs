using DataRetriever.DataStorage;

namespace DataRetriever.Factory
{
  public interface IDataStorageFactory
  {
    IDataStorage CreateDataSource(DataStorageType sourceType);
  }
}