using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using App.Application.Auth.Configurations;
using App.Application.Auth.Constants;
using App.Application.Auth.Services;
using App.Domain.Entities;

using ErrorOr;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.Infrastructure.Auth.Services;

public class TokenService : ITokenService
{
  private readonly JwtSettings _jwtSettings;

  public TokenService(IOptions<JwtSettings> jwtSettings)
  {
    _jwtSettings = jwtSettings.Value;
  }

  public string GenerateAccessToken(User user)
  {
    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString(CultureInfo.InvariantCulture)),
      new Claim(JwtRegisteredClaimNames.Email, user.Email),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(CustomClaimTypes.USERNAME, user.Username),
      new Claim(CustomClaimTypes.TOKEN_VERSION, user.TokenVersion.ToString(CultureInfo.InvariantCulture)),
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      _jwtSettings.Issuer,
      _jwtSettings.Audience,
      claims,
      expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
      signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public string GenerateRefreshToken()
  {
    return Guid.NewGuid().ToString();
  }

  public ErrorOr<bool> ValidateAccessToken(string token)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

    var result = tokenHandler.ValidateToken(token, new TokenValidationParameters
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(key),
      ValidateIssuer = true,
      ValidIssuer = _jwtSettings.Issuer,
      ValidateAudience = true,
      ValidAudience = _jwtSettings.Audience,
      ValidateLifetime = true,
      ClockSkew = TimeSpan.Zero
    }, out _);

    return result != null;
  }

  public ErrorOr<int?> GetUserIdFromAccessToken(string token)
  {
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var jsonToken = tokenHandler.ReadJwtToken(token);

      var userIdClaim = jsonToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

      if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        return Error.Validation(AuthErrors.Token.INVALID_USER_ID, "Invalid user ID in token");

      return userId;
    }
    catch
    {
      return Error.Validation(AuthErrors.Token.INVALID_TOKEN, "Invalid token format");
    }
  }

  public ErrorOr<int?> GetTokenVersionFromAccessToken(string token)
  {
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var jsonToken = tokenHandler.ReadJwtToken(token);

      var tokenVersionClaim = jsonToken.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.TOKEN_VERSION)?.Value;

      if (tokenVersionClaim == null || !int.TryParse(tokenVersionClaim, out var tokenVersion))
        return Error.Validation(AuthErrors.Token.INVALID_TOKEN, "Invalid token version in token");

      return tokenVersion;
    }
    catch
    {
      return Error.Validation(AuthErrors.Token.INVALID_TOKEN, "Invalid token format");
    }
  }
}