namespace postit_csharp.Models;

public class Collaborator
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public int AlbumId { get; set; }
  public string AccountId { get; set; }
}