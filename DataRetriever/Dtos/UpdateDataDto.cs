namespace DataRetriever.Dtos
{
  /// <summary>
  /// Represents the data transfer object for updating data.
  /// </summary>
  public class UpdateDataDto
  {
    /// <summary>
    /// Gets or sets the value of the data to be updated.
    /// </summary>
    public required string Value { get; set; }
  }
}