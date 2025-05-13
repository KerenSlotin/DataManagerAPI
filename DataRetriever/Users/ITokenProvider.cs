namespace DataRetriever.Users
{
  public interface ITokenProvider
  {
    string Create(string username, string password);
  }
}