namespace postit_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollaboratorsController : ControllerBase
{

  private readonly Auth0Provider _auth0Provider;
  private readonly CollaboratorsService _collaboratorsService;

  public CollaboratorsController(Auth0Provider auth0Provider, CollaboratorsService collaboratorsService)
  {
    _auth0Provider = auth0Provider;
    _collaboratorsService = collaboratorsService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Collaborator>> CreateCollaborator([FromBody] Collaborator collaboratorData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      collaboratorData.AccountId = userInfo.Id;
      Collaborator collaborator = _collaboratorsService.CreateCollaborator(collaboratorData);
      return Ok(collaborator);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{collaboratorId}")]
  public async Task<ActionResult<string>> DestroyCollaborator(int collaboratorId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      string message = _collaboratorsService.DestroyCollaborator(collaboratorId, userId);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}