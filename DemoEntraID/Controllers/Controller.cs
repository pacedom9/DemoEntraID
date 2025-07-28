using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoEntraID.Controllers;

[Route("api/demo/entraid")]
[ApiController]
[Authorize]
public class Controller : ControllerBase
{
  // GET: api/<Controller>
  [HttpGet]
  public IActionResult Get() => Ok("Api funzionante");
}
