



namespace postit_csharp.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repository;

  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }

  internal Album ArchiveAlbum(int albumId, string userId)
  {
    Album album = GetAlbumById(albumId);

    if (album.CreatorId != userId)
    {
      throw new Exception("NOT YOUR ALBUM");
    }

    if (album.Archived)
    {
      return album;
    }

    Album archivedAlbum = _repository.ArchiveAlbum(albumId);

    return archivedAlbum;
  }

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repository.CreateAlbum(albumData);
    return album;
  }

  internal Album GetAlbumById(int albumId)
  {
    Album album = _repository.GetAlbumById(albumId);

    if (album == null)
    {
      throw new Exception($"Invalid id: {albumId}");
    }

    return album;
  }

  internal List<Album> GetAlbums()
  {
    List<Album> albums = _repository.GetAlbums();

    return albums;
  }
}