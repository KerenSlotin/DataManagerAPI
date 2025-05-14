using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DataRetriever.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace DataRetriever.DataStorage
{
  /// <summary>
  /// Interface for managing data storage operations.
  /// </summary>
  public interface IDataStorage
  {
    internal DataStorageType StorageType { get; }
    internal Task<DataItem?> GetDataAsync(string id);
    internal Task SaveDataAsync(DataItem dataItem);
    internal Task UpdateDataAsync(DataItem dataItem);
  }
}