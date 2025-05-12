
using System.Reflection.Metadata.Ecma335;
using DataRetriever.DataStorage;

namespace DataRetriever.Factory
{
  public class DataStorageFactory : IDataStorageFactory
  {
    private readonly Dictionary<DataStorageType, Func<IDataStorage>> _dataStorages;

    public DataStorageFactory(IServiceProvider serviceProvider)
    {
      _dataStorages = new Dictionary<DataStorageType, Func<IDataStorage>>
        {
            { DataStorageType.Cache, serviceProvider.GetRequiredService<CacheStorage> },
            { DataStorageType.File, serviceProvider.GetRequiredService<FileStorage> },
            { DataStorageType.Database, serviceProvider.GetRequiredService<DbStorage> }
        };
    }

    public IDataStorage CreateDataSource(DataStorageType sourceType)
    {
      if (_dataStorages.TryGetValue(sourceType, out var getDataStorage))
      {
        return getDataStorage();
      }

      throw new ArgumentOutOfRangeException(nameof(sourceType), sourceType, null);
    }
  }
}