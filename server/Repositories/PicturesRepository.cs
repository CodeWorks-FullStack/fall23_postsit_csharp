
namespace postit_csharp.Repositories;

public class PicturesRepository
{

  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
      INSERT INTO
      pictures(imgUrl, albumId, creatorId)
      VALUES(@ImgUrl, @AlbumId, @CreatorId);
      
      SELECT * FROM pictures WHERE id = LAST_INSERT_ID();";

    Picture picture = _db.Query<Picture>(sql, pictureData).FirstOrDefault();
    return picture;
  }
}