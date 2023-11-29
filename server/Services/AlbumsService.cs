



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

    // NOTE check ownership
    if (album.CreatorId != userId)
    {
      throw new Exception("NOT YOUR ALBUM");
    }

    // NOTE cannot unarchive album, so we don't call to our repo
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