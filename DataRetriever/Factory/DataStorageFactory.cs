
using System.Reflection.Metadata.Ecma335;
using DataRetriever.DataStorage;

namespace DataRetriever.Factory
{
  internal class DataStorageFactory : IDataStorageFactory
  {
    private readonly IEnumerable<IDataStorage> _dataStorages;

    public DataStorageFactory(IEnumerable<IDataStorage> dataStorages)
    {
      _dataStorages = dataStorages;
    }

    public IDataStorage CreateDataStorage(DataStorageType sourceType)
    {
      var storage = _dataStorages.FirstOrDefault(s => s.StorageType == sourceType);
      return storage ?? throw new ArgumentOutOfRangeException(nameof(sourceType), sourceType, null);
    }
    
    public IEnumerable<DataStorageType> GetAllDataStorageTypes()
    {
      return _dataStorages.Select(s => s.StorageType).Distinct();
    }
  }
}