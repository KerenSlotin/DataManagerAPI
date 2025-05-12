using DataRetriever.Models;

namespace DataRetriever.DataStorage
{
  public class FileStorage : IDataStorage
  {
    private const string _filePath = "data.txt";

    public async Task<DataItem> GetDataAsync(Guid id)
    {
      var lines = await File.ReadAllLinesAsync(_filePath);
      foreach (var line in lines)
      {
        var parts = line.Split(',');
        if (Guid.TryParse(parts[0], out var dataId) && dataId == id)
        {
          return new DataItem
          {
            Id = dataId,
            Value = parts[1],
            CreatedAt = DateTime.Parse(parts[2])
          };
        }
      }
      return null;
    }
  }
}