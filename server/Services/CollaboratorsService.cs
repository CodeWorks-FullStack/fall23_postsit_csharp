

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

  internal List<Collaborator> GetCollaboratorsByAlbumId(int albumId)
  {
    List<Collaborator> collaborators = _repository.GetCollaboratorsByAlbumId(albumId);
    return collaborators;
  }
}