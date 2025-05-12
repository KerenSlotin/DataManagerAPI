using DataRetriever.Data;
using DataRetriever.Models;
using DataRetriever.Services.Interfaces;

namespace DataRetriever.Services.Concrete
{
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
}