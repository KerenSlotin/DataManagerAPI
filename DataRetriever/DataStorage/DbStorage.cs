using DataRetriever.Repository;

namespace DataRetriever.DataStorage
{
  public class DbStorage : IDataStorage
  {
    public DataStorageType StorageType => DataStorageType.Database;
    private readonly IDataRepository _repository;
    public DbStorage(IDataRepository repository)
    {
      _repository = repository;
    }

    public async Task<DataItem?> GetDataAsync(string id)
    {
      return await _repository.GetDataAsync(id);
    }

    public async Task SaveDataAsync(DataItem dataItem)
    {
      await _repository.AddAsync(dataItem);
    }

    public async Task UpdateDataAsync(DataItem dataItem)
    {
      await _repository.UpdateAsync(dataItem);
    }
  }
}