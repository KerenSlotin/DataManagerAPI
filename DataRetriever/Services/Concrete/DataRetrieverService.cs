using DataRetriever.DataStorage;
using DataRetriever.Dtos;
using DataRetriever.Factory;
using DataRetriever.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataRetriever.Services.Concrete
{
  internal class DataRetrieverService : IDataRetrieverService
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

    public async Task<DataItem> CreateDataAsync(CreateDataDto data)
    {
      var dbStorage = _dataStorageFactory.CreateDataStorage(DataStorageType.Database);
      var dataItem = new DataItem 
      {
        Value = data.Value,
      };

      await dbStorage.SaveDataAsync(dataItem);
      return dataItem;
    }

    public async Task UpdateDataAsync(string id, UpdateDataDto data)
    {
      foreach (var dataStorageType in Enum.GetValues(typeof(DataStorageType)).Cast<DataStorageType>())
      {
        var dataStorage = _dataStorageFactory.CreateDataStorage(dataStorageType);
        var dataItem = await dataStorage.GetDataAsync(id);
        
        if (dataItem == null)
          continue;

        dataItem.Value = data!.Value;
        await dataStorage.UpdateDataAsync(dataItem);
      }
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