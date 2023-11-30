namespace postit_csharp.Models;

public class ProfileCollaboration : Profile
{
  public int CollaborationId { get; set; }
  public int AlbumId { get; set; }

  // NOTE everything below are properties of a profile, these are inhereited from the profile class
  // public string Id { get; set; }
  // public DateTime CreatedAt { get; set; }
  // public DateTime UpdatedAt { get; set; }
  // public string Name { get; set; }
  // public string Picture { get; set; }
}