
public interface IDataStorageFactory
{
  IDataStorage CreateDataSource(DataStorageType sourceType);
}