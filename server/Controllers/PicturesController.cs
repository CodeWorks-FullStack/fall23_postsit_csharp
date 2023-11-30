namespace postit_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicturesController : ControllerBase
{

  private readonly Auth0Provider _auth0Provider;
  private readonly PicturesService _picturesService;

  public PicturesController(Auth0Provider auth0Provider, PicturesService picturesService)
  {
    _auth0Provider = auth0Provider;
    _picturesService = picturesService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      pictureData.CreatorId = userInfo.Id;
      Picture picture = _picturesService.CreatePicture(pictureData);
      return Ok(picture);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}