
public class DataStorageFactory : IDataStorageFactory
{
    public IDataStorage CreateDataSource(DataStorageType sourceType)
    {
        return sourceType switch
        {
            DataStorageType.Cache => new CacheStorage(),
            DataStorageType.File => new FileStorage(),
            DataStorageType.Database => new DbStorage(),
            _ => throw new ArgumentOutOfRangeException(nameof(sourceType), sourceType, null)
        };
    }
}