
using DataRetriever.Dtos;

namespace DataRetriever.Services.Interfaces
{
  public interface IDataRetrieverService
  {
    Task<DataItem?> GetDataAsync(string id);
    Task<DataItem> CreateDataAsync(CreateDataDto dataDto);
    Task UpdateDataAsync(string id, UpdateDataDto data);
  }
}