using DataRetriever.Models;

public interface IDataRetrieverService
{
    Task<DataItem> GetDataAsync(Guid id);
}