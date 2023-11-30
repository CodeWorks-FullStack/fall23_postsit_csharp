



namespace postit_csharp.Services;

public class CollaboratorsService
{
  private readonly CollaboratorsRepository _repository;

  public CollaboratorsService(CollaboratorsRepository repository)
  {
    _repository = repository;
  }

  internal Collaborator CreateCollaborator(Collaborator collaboratorData)
  {
    Collaborator collaborator = _repository.CreateCollaborator(collaboratorData);
    return collaborator;
  }

  internal Collaborator GetCollaboratorById(int collaboratorId)
  {
    Collaborator collaborator = _repository.GetCollaboratorById(collaboratorId);
    if (collaborator == null)
    {
      throw new Exception($"Invalid id: {collaboratorId}");
    }
    return collaborator;
  }

  internal string DestroyCollaborator(int collaboratorId, string userId)
  {
    Collaborator collaborator = GetCollaboratorById(collaboratorId);
    if (collaborator.AccountId != userId)
    {
      throw new Exception("NOT YOUR COLLAB");
    }

    _repository.DestroyCollaborator(collaboratorId);
    return "IT IS GONE";
  }

  internal List<AlbumCollaboration> GetAlbumCollaborationsByAccountId(string userId)
  {
    List<AlbumCollaboration> albumCollaborations = _repository.GetAlbumCollaborationsByAccountId(userId);
    return albumCollaborations;
  }

  internal List<ProfileCollaboration> GetCollaboratorsByAlbumId(int albumId)
  {
    List<ProfileCollaboration> collaborators = _repository.GetCollaboratorsByAlbumId(albumId);
    return collaborators;
  }
}