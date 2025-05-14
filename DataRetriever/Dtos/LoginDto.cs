namespace DataRetriever.Dtos
{
  /// <summary>
  /// Represents the data transfer object for user login credentials.
  /// </summary>
  public class LoginDto
  {
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public required string Password { get; set; }
  }
}