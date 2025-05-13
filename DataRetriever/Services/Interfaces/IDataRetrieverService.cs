
namespace DataRetriever.Services.Interfaces
{
  public interface IDataRetrieverService
  {
    Task<DataItem?> GetDataAsync(string id);
    Task<DataItem> CreateData(CreateDataDto dataDto);
  }
}