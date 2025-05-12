using System.Text.Json;
using DataRetriever.Models;

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
                .Where(IsExpired).FirstOrDefault();

      if (files == null || files.Length == 0)
        return null;

      var fileContent = await File.ReadAllTextAsync(files);
      return JsonSerializer.Deserialize<DataItem>(fileContent);
    }

    public async Task<bool> SaveDataAsync(DataItem data)
    {
      var expirationTime = DateTime.UtcNow.AddMinutes(30);
      var fileName = Path.Combine(_storagePath, $"{data.Id}_{expirationTime.Ticks}.json");

      await File.WriteAllTextAsync(fileName, JsonSerializer.Serialize(data));
      return true;
    }

    private bool IsExpired(string fileName)
    {
      var ticks = long.Parse(Path.GetFileNameWithoutExtension(fileName).Split('_')[1]);
      var expirationTime = new DateTime(ticks);
      return expirationTime < DateTime.UtcNow;
    }
  }
}