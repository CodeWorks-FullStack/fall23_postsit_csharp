

namespace postit_csharp.Repositories;

public class CollaboratorsRepository
{
  private readonly IDbConnection _db;

  public CollaboratorsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Collaborator CreateCollaborator(Collaborator collaboratorData)
  {
    string sql = @"
    INSERT INTO
    collaborators(albumId, accountId)
    VALUES(@AlbumId, @AccountId);
    
    SELECT * FROM collaborators WHERE id = LAST_INSERT_ID();";

    Collaborator collaborator = _db.Query<Collaborator>(sql, collaboratorData).FirstOrDefault();

    return collaborator;
  }

  internal List<Collaborator> GetCollaboratorsByAlbumId(int albumId)
  {
    string sql = @"
    SELECT * FROM collaborators WHERE albumId = @albumId
    ;";

    List<Collaborator> collaborators = _db.Query<Collaborator>(sql, new { albumId }).ToList();
    return collaborators;
  }
}