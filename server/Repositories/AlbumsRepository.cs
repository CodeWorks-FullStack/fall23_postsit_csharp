
namespace postit_csharp.Repositories;

public class AlbumsRepository
{

  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO 
    albums(title, category, coverImg, creatorId)
    VALUES(@Title, @Category, @CoverImg, @CreatorId);
    
    SELECT 
    alb.*,
    acc.*
    FROM albums alb
    JOIN accounts acc ON alb.creatorId = acc.id
    WHERE alb.id = LAST_INSERT_ID();";

    // Album album = _db.Query<Album>(sql, albumData).FirstOrDefault();

    // Album album = _db.Query<Album, Profile, Album>(sql, AlbumBuilder, albumData).FirstOrDefault();


    Album album = _db.Query<Album, Profile, Album>(sql, (album, profile) =>
    {
      album.Creator = profile;
      return album;
    }, albumData).FirstOrDefault();

    return album;
  }

  private Album AlbumBuilder(Album album, Profile profile)
  {
    album.Creator = profile;
    return album;
  }
}