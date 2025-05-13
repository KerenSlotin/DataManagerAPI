using System.Text.Json;

namespace DataRetriever.DataStorage
{
  public class FileStorage : IDataStorage
  {
    public DataStorageType StorageType => DataStorageType.File;

    private readonly string _storagePath;

    public FileStorage(IConfiguration configuration)
    {
      _storagePath = configuration["FileStorage:Path"] ?? 
                Path.Combine(Directory.GetCurrentDirectory(), "TempStorage");
      if (!Directory.Exists(_storagePath))
        Directory.CreateDirectory(_storagePath);
    }

    public async Task<DataItem?> GetDataAsync(string id)
    {
      var files = Directory.GetFiles(_storagePath, $"{id}_*.json")
                .Where(file => !IsExpired(file)).FirstOrDefault();

      if (files == null || files.Length == 0)
        return null;

      var fileContent = await File.ReadAllTextAsync(files);
      return JsonSerializer.Deserialize<DataItem>(fileContent);
    }

    public async Task SaveDataAsync(DataItem data)
    {
      var expirationTime = DateTime.UtcNow.AddMinutes(30);
      var fileName = Path.Combine(_storagePath, $"{data.Id}_{expirationTime.Ticks}.json");

      await File.WriteAllTextAsync(fileName, JsonSerializer.Serialize(data));
    }

    public async Task UpdateDataAsync(DataItem dataItem)
    {
      var existingFile = Directory.GetFiles(_storagePath, $"{dataItem.Id}_*.json").FirstOrDefault();
      if (existingFile != null)
      {
        File.Delete(existingFile);
        await SaveDataAsync(dataItem);
      }
    }

    private bool IsExpired(string fileName)
    {
      var ticks = long.Parse(Path.GetFileNameWithoutExtension(fileName).Split('_')[1]);
      var expirationTime = new DateTime(ticks);

      if (DateTime.Compare(expirationTime, DateTime.UtcNow) > 0)
      {
        return false;
      }
      else
      {
        File.Delete(fileName);
        return true;
      }
    }

  }
}