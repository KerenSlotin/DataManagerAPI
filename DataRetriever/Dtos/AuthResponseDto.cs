namespace DataRetriever.Dtos
{

  /// <summary>
  /// Represents the response from the authentication endpoint.
  /// </summary>
  public class AuthResponseDto
  {
    /// <summary>
    /// Gets or sets the token used for authentication.
    /// </summary>
    public string? Token { get; set; }
  }
}