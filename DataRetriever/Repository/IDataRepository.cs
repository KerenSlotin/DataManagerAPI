
namespace DataRetriever.Repository
{
  public interface IDataRepository
  {
    Task<DataItem?> GetDataAsync(string id);
    Task AddAsync(DataItem dataItem);
    Task<bool> UpdateAsync(DataItem dataItem);
  }
}