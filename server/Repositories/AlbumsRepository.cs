namespace postit_csharp.Repositories;

public class AlbumsRepository
{

  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Album ArchiveAlbum(int albumId)
  {
    // NOTE soft delete
    string sql = @"
    UPDATE albums
    SET
    archived = true
    WHERE id = @albumId;
    
    SELECT
    alb.*,
    acc.*
    FROM albums alb
    JOIN accounts acc ON acc.id = alb.creatorId
    WHERE alb.id = @albumId;";

    Album album = _db.Query<Album, Profile, Album>(sql, (album, profile) =>
    {
      album.Creator = profile;
      return album;
    }, new { albumId }).FirstOrDefault();
    return album;
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

    // NOTE we can't do this because we are dealing with joined rows
    // Album album = _db.Query<Album>(sql, albumData).FirstOrDefault();

    // NOTE top developer tip that instructors don't want you to know about!
    // Album album = _db.Query<Album, Profile, Album>(sql, AlbumBuilder, albumData).FirstOrDefault();


    // NOTE first type passed to Query<> is the first data type coming in on our row from sql join, second is the second data type coming in our our row from sql join, and the third type is our return type
    // NOTE second argument passed to query is our mapping function. Since we have multiple pieces of data coming in on the same row, we have to tell dapper what to do with these. In this case, we want to assign the album's creator property to the account information coming from our sql join
    Album album = _db.Query<Album, Profile, Album>(sql, (album, profile) =>
    {
      album.Creator = profile;
      return album;
    }, albumData).FirstOrDefault();

    return album;
  }

  internal Album GetAlbumById(int albumId)
  {
    string sql = @"
    SELECT
    alb.*,
    acc.*
    FROM albums alb
    JOIN accounts acc ON alb.creatorId = acc.id
    WHERE alb.id = @albumId;";

    Album album = _db.Query<Album, Profile, Album>(sql, (album, profile) =>
    {
      album.Creator = profile;
      return album;
    }, new { albumId }).FirstOrDefault();
    return album;
  }

  internal List<Album> GetAlbums()
  {
    string sql = @"
    SELECT 
    alb.*,
    acc.* 
    FROM albums alb
    JOIN accounts acc ON alb.creatorId = acc.id;";

    List<Album> albums = _db.Query<Album, Profile, Album>(sql, (album, profile) =>
    {
      album.Creator = profile;
      return album;
    }).ToList();
    return albums;
  }

  private Album AlbumBuilder(Album album, Profile profile)
  {
    album.Creator = profile;
    return album;
  }
}