
namespace postit_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{

  private readonly AlbumsService _albumsService;
  private readonly Auth0Provider _auth0Provider; // NOTE brings in the built in auth0provider, don't forget to add this to your constructor

  public AlbumsController(AlbumsService albumsService, Auth0Provider auth0Provider)
  {
    _albumsService = albumsService;
    _auth0Provider = auth0Provider;
  }

  [Authorize] // NOTE the below method requires a bearer token to access
  [HttpPost]
  public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      // NOTE brings in information from auth0, similar to req.userInfo
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      // REVIEW don't trust the user to tell us who they are
      albumData.CreatorId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet]
  public ActionResult<List<Album>> GetAlbums()
  {
    try
    {
      List<Album> albums = _albumsService.GetAlbums();
      return Ok(albums);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetAlbumById(int albumId)
  {
    try
    {
      Album album = _albumsService.GetAlbumById(albumId);
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{albumId}")]
  public async Task<ActionResult<Album>> ArchiveAlbum(int albumId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      Album album = _albumsService.ArchiveAlbum(albumId, userId);
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
