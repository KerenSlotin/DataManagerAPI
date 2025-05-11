using DataRetriever.Data;
using DataRetriever.Models;

public class DataRetrieverService : IDataRetrieverService
{
    private readonly IDataRepository _dataRetrieverRepository;

    public DataRetrieverService(IDataRepository dataRetrieverRepository)
    {
      _dataRetrieverRepository = dataRetrieverRepository;
    }

    public Task<DataItem> GetDataAsync(Guid id)
    {
      throw new NotImplementedException();
    }
}