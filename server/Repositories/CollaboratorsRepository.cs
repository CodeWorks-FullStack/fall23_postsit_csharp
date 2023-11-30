




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

  internal void DestroyCollaborator(int collaboratorId)
  {
    string sql = "DELETE FROM collaborators WHERE id = @collaboratorId LIMIT 1;";
    _db.Execute(sql, new { collaboratorId });
  }

  internal List<AlbumCollaboration> GetAlbumCollaborationsByAccountId(string userId)
  {
    string sql = @"
    SELECT
    collab.*,
    alb.*,
    acc.*
    FROM collaborators collab
    JOIN albums alb ON collab.albumId = alb.id
    JOIN accounts acc ON acc.id = alb.creatorId
    WHERE collab.accountId = @userId;";

    List<AlbumCollaboration> albumCollaborations = _db.Query<Collaborator, AlbumCollaboration, Profile, AlbumCollaboration>
    (sql, (collaborator, albumCollaboration, profile) =>
    {
      albumCollaboration.CollaborationId = collaborator.Id;
      albumCollaboration.AccountId = collaborator.AccountId;
      albumCollaboration.Creator = profile;
      return albumCollaboration;
    }, new { userId }).ToList();
    return albumCollaborations;
  }

  internal Collaborator GetCollaboratorById(int collaboratorId)
  {
    string sql = "SELECT * FROM collaborators WHERE id = @collaboratorId;";

    Collaborator collaborator = _db.Query<Collaborator>(sql, new { collaboratorId }).FirstOrDefault();
    return collaborator;
  }

  internal List<ProfileCollaboration> GetCollaboratorsByAlbumId(int albumId)
  {
    string sql = @"
    SELECT 
    collab.*,
    acc.*
    FROM collaborators collab 
    JOIN accounts acc ON acc.id = collab.accountId
    WHERE collab.albumId = @albumId;";

    List<ProfileCollaboration> collaborators = _db.Query<Collaborator, ProfileCollaboration, ProfileCollaboration>
    (sql, (collaborator, profileCollab) =>
    {
      profileCollab.CollaborationId = collaborator.Id;
      profileCollab.AlbumId = collaborator.AlbumId;
      return profileCollab;
    },
     new { albumId }).ToList();
    return collaborators;
  }
}