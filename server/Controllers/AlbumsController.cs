
namespace postit_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{

  private readonly AlbumsService _albumsService;
  private readonly Auth0Provider _auth0Provider;

  public AlbumsController(AlbumsService albumsService, Auth0Provider auth0Provider)
  {
    _albumsService = albumsService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      albumData.CreatorId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
