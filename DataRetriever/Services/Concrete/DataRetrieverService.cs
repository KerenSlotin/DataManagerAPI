using DataRetriever.DataStorage;
using DataRetriever.Factory;
using DataRetriever.Models;
using DataRetriever.Services.Interfaces;

namespace DataRetriever.Services.Concrete
{
  public class DataRetrieverService : IDataRetrieverService
  {
    private readonly IDataStorageFactory _dataStorageFactory;
    public DataRetrieverService(IDataStorageFactory dataStorageFactory)
    {
      _dataStorageFactory = dataStorageFactory;
    }

    public async Task<DataItem?> GetDataAsync(string id)
    {
      foreach(var dataStorageType in Enum.GetValues(typeof(DataStorageType)).Cast<DataStorageType>())
      {
        var dataStorage = _dataStorageFactory.CreateDataStorage(dataStorageType);
        var data = await dataStorage.GetDataAsync(id);
        if (data != null)
        {
          if (dataStorage.StorageType != DataStorageType.Cache)
            await UpdateCache(data);
          return data;
        }
      }
    
      return null;
    }

    private async Task UpdateCache(DataItem data)
    {
      var cache =  _dataStorageFactory.CreateDataStorage(DataStorageType.Cache);
      await cache.SaveDataAsync(data);
    }
  }
}