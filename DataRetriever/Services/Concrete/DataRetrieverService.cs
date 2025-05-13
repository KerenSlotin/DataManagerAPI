using DataRetriever.DataStorage;
using DataRetriever.Factory;
using DataRetriever.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
          if (dataStorage.StorageType == DataStorageType.Database)
            await UpdateFile(data);
          return data;
        }
      }
    
      return null;
    }

    public async Task<DataItem> CreateData(CreateDataDto dataDto)
    {
      var dbStorage = _dataStorageFactory.CreateDataStorage(DataStorageType.Database);
      var dataItem = new DataItem 
      {
        Value = dataDto.Value,
      };

      await dbStorage.SaveDataAsync(dataItem);
      return dataItem;
    }


    private async Task UpdateCache(DataItem data)
    {
      var cache =  _dataStorageFactory.CreateDataStorage(DataStorageType.Cache);
      await cache.SaveDataAsync(data);
    }

    private async Task UpdateFile(DataItem data)
    {
      var fileStorage = _dataStorageFactory.CreateDataStorage(DataStorageType.File);
      await fileStorage.SaveDataAsync(data);
    }
  }
}