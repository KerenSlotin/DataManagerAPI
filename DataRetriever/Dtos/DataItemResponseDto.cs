namespace DataRetriever.Dtos
{
  /// <summary>
  /// Represents the response from the authentication endpoint.
  /// </summary>
  public class DataItemResponseDto
  {
    internal string? Id { get; set; }
    internal string? Value { get; set; }
    internal DateTime CreatedAt { get; set; }
  }
}