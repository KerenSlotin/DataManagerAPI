namespace DataRetriever.Models;

public class DataItem
{
  public Guid Id { get; set; }
  public string? Value { get; set; }
  public DateTime CreatedAt { get; set; }
}
