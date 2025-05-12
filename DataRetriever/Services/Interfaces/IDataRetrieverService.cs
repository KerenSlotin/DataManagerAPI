using DataRetriever.Models;

namespace DataRetriever.Services.Interfaces
{
  public interface IDataRetrieverService
  {
    Task<DataItem?> GetDataAsync(string id);
  }
}