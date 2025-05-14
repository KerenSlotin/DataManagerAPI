
using DataRetriever.Dtos;

namespace DataRetriever.Services.Interfaces
{
  /// <summary>
  /// Interface for the data retrieval service.
  /// </summary>
  public interface IDataRetrieverService
  {
    internal Task<DataItem?> GetDataAsync(string id);
    internal Task<DataItem> CreateDataAsync(CreateDataDto dataDto);
    internal Task UpdateDataAsync(string id, UpdateDataDto data);
  }
}