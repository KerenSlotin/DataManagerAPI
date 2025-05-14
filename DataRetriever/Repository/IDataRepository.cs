
namespace DataRetriever.Repository
{
  /// <summary>
  /// Interface for managing data repository.
  /// </summary>
  public interface IDataRepository
  {
    internal Task<DataItem?> GetDataAsync(string id);
    internal Task AddAsync(DataItem dataItem);
    internal Task<bool> UpdateAsync(DataItem dataItem);
  }
}