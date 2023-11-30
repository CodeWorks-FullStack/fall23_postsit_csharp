


namespace postit_csharp.Services;
public class PicturesService
{
  private readonly PicturesRepository _repository;

  public PicturesService(PicturesRepository repository)
  {
    _repository = repository;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    Picture picture = _repository.CreatePicture(pictureData);
    return picture;
  }

  internal Picture GetPictureById(int pictureId)
  {
    Picture picture = _repository.GetPictureById(pictureId);
    if (picture == null)
    {
      throw new Exception($"Invalid id: {pictureId}");
    }
    return picture;
  }

  internal string DestroyPicture(int pictureId, string userId)
  {
    Picture picture = GetPictureById(pictureId);
    if (picture.CreatorId != userId)
    {
      throw new Exception("NOT YOUR PICTURE");
    }
    _repository.DestroyPicture(pictureId);
    return "Picture was deleted!";
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }
}