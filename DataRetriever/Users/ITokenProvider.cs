namespace DataRetriever.Users
{
  /// <summary>
  /// This interface is responsible for creating tokens based on user credentials.
  /// </summary>
  public interface ITokenProvider
  {
    internal string Create(string username, string password);
  }
}