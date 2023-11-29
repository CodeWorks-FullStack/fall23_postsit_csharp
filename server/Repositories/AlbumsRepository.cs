
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
    alb.*
    FROM albums alb
    JOIN accounts acc ON alb.creatorId =
    WHERE id = LAST_INSERT_ID();";

    Album album = _db.Query<Album>(sql, albumData).FirstOrDefault();
    return album;
  }
}