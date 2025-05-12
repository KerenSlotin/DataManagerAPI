using DataRetriever.Models;

namespace DataRetriever.DataStorage
{
  public class FileStorage : IDataStorage
  {
    public DataStorageType StorageType => DataStorageType.File;

    private const string _filePath = "data.txt";

    public async Task<DataItem> GetDataAsync(string id)
    {
      var lines = await File.ReadAllLinesAsync(_filePath);
      foreach (var line in lines)
      {
        var parts = line.Split(',');
        if (parts[0] == id)
        {
          return new DataItem
          {
            Id = id,
            Value = parts[1],
            CreatedAt = DateTime.Parse(parts[2])
          };
        }
      }
      return null;
    }

    public Task<bool> SaveDataAsync(DataItem dataItem)
    {
      throw new NotImplementedException();
    }
  }
}