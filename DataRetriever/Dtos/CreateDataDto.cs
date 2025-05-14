namespace DataRetriever.Dtos
{
  /// <summary>
  /// Represents the data transfer object for creating new data.
  /// </summary>
  public class CreateDataDto
  {
    /// <summary>
    /// Gets or sets the value of the data to be created.
    /// </summary>
    public required string Value { get; set; }
  }
}