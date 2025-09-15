namespace App.Application.User.Auth.Services;

public interface IAuthCookieService
{
  void SetAccessTokenCookie(string accessToken);
  void SetRefreshTokenCookie(string refreshToken);
  void RemoveAuthCookies();
  string? GetAccessTokenFromCookie();
  string? GetRefreshTokenFromCookie();
}