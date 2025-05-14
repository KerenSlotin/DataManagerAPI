
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace DataRetriever.Users
{
  internal class TokenProvider : ITokenProvider
  {
    private readonly IConfiguration _configuration;
    private readonly Dictionary<string, string> _users;
    private readonly Dictionary<string, string> _userRoles;
    public TokenProvider(IConfiguration configuration)
    {
      _configuration = configuration;
      _users = new Dictionary<string, string>
        {
          { "admin", "admin123" },
          { "user", "user123" }
        };

      _userRoles = new Dictionary<string, string>
      {
        { "admin", "Admin" },
        { "user", "User" }
      };
    }

    public string Create(string username, string password)
    {
      if(!_users.TryGetValue(username, out var storedPassword) || storedPassword != password)
      {
        throw new UnauthorizedAccessException("Invalid username or password");
      }

      var jwtConfig = _configuration.GetSection("Jwt");
      var key = jwtConfig["Key"] ?? throw new ArgumentNullException("Key", "JWT key is not configured");
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
          {
              new Claim(ClaimTypes.Name, username),
              new Claim(ClaimTypes.Role, _userRoles[username])
          }),
        Expires = DateTime.UtcNow.AddMinutes(jwtConfig.GetValue<int>("ExpirationInMinutes")),
        SigningCredentials = credentials,
      };

      var tokenHandler = new JsonWebTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return token;
    }
  }
}