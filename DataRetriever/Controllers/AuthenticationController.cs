using DataRetriever.Dtos;
using DataRetriever.Users;
using FluentValidation;
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
    private IValidator<LoginDto> _validator;
    private ITokenProvider _tokenprovider;

    /// <summary>
    /// Constructor for AuthenticationController.
    /// </summary>
    /// <param name="tokenprovider"></param>
    /// <param name="validator"></param>
    public AuthenticationController(ITokenProvider tokenprovider, IValidator<LoginDto> validator)
    {
      _tokenprovider = tokenprovider;
      _validator = validator;
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
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginData)
    {
      var result = await _validator.ValidateAsync(loginData);
      if (!result.IsValid)
      {
        return BadRequest(result.Errors);
      }

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