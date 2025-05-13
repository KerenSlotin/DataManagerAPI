using DataRetriever.Dtos;
using DataRetriever.Users;
using Microsoft.AspNetCore.Mvc;

namespace DataRetriever.Controllers
{
  [Route("users")]
  public class AuthenticationController : ControllerBase
  {
    public ITokenProvider _tokenprovider;
    public AuthenticationController(ITokenProvider tokenprovider)
    {
      _tokenprovider = tokenprovider;
    }

    [HttpPost("login")]
    public ActionResult<AuthResponseDto> Login([FromBody] LoginDto loginData)
    {
      var username = loginData?.Username;
      var password = loginData?.Password;
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
      {
        return BadRequest();
      }

      try
      {
        var token = _tokenprovider.Create(username, password);
        return Ok(new AuthResponseDto
        {
          Token = token
        });
      }
      catch (UnauthorizedAccessException)
      {
        return Unauthorized();
      }
    }
  }
}