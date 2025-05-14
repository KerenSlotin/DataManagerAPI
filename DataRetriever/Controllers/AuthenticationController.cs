using DataRetriever.Dtos;
using DataRetriever.Users;
using Microsoft.AspNetCore.Mvc;

namespace DataRetriever.Controllers
{
  /// <summary>
  /// Controller responsible for user authentication.
  /// Provides an endpoint to log in and receive a JWT token.
  /// </summary>
  [Route("users")]
  public class AuthenticationController : ControllerBase
  {
    private ITokenProvider _tokenprovider;

    /// <summary>
    /// Constructor for AuthenticationController.
    /// </summary>
    /// <param name="tokenprovider"></param>
    public AuthenticationController(ITokenProvider tokenprovider)
    {
      _tokenprovider = tokenprovider;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token upon successful login.
    /// </summary>
    /// <param name="loginData">The login credentials (username and password).</param>
    /// <returns>
    /// Returns a 200 OK with the authentication token if successful.  
    /// Returns 400 Bad Request if credentials are missing.  
    /// Returns 401 Unauthorized if credentials are invalid.
    /// </returns>
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