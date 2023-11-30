

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
      
      SELECT 
      pic.*,
      acc.* 
      FROM pictures pic
      JOIN accounts acc ON acc.id = pic.creatorId
      WHERE pic.id = LAST_INSERT_ID();";

    Picture picture = _db.Query<Picture, Profile, Picture>(sql, (picture, profile) =>
    {
      picture.Creator = profile;
      return picture;
    }, pictureData).FirstOrDefault();
    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    pic.*,
    acc.*
    FROM pictures pic
    JOIN accounts acc ON acc.id = pic.creatorId 
    WHERE pic.albumId = @albumId;";

    List<Picture> pictures = _db.Query<Picture, Profile, Picture>(sql, (picture, profile) =>
    {
      picture.Creator = profile;
      return picture;
    }, new { albumId }).ToList();

    return pictures;
  }
}