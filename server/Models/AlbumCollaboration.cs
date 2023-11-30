namespace postit_csharp.Models;
public class AlbumCollaboration : Album
{
  public int CollaborationId { get; set; }
  public string AccountId { get; set; }

  // NOTE all album members brought in through inheritance
}