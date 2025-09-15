using System.Security.Claims;

using App.Application.Common.Data;
using App.Application.User.Auth.Configurations;
using App.Application.User.Auth.Constants;
using App.Domain.Enums;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using UserDomain = App.Domain.Entities.User;

namespace App.Infrastructure.User.Auth.Events;

public class JwtEvents : JwtBearerEvents
{
  private readonly IServiceProvider _serviceProvider;

  public JwtEvents(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public override async Task MessageReceived(MessageReceivedContext context)
  {
    if (string.IsNullOrEmpty(context.Token))
    {
      using var scope = _serviceProvider.CreateScope();
      var cookieSettings = scope.ServiceProvider.GetRequiredService<IOptions<AuthCookieSettings>>().Value;

      if (context.Request.Cookies.TryGetValue(cookieSettings.AccessTokenCookieName, out var accessToken)
          && !string.IsNullOrEmpty(accessToken))
      {
        context.Token = accessToken;
      }
    }

    await base.MessageReceived(context);
  }

  public override async Task TokenValidated(TokenValidatedContext context)
  {
    using var scope = _serviceProvider.CreateScope();
    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

    var userIdClaim = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var tokenVersionClaim = context.Principal?.FindFirst(CustomClaimTypes.TOKEN_VERSION)?.Value;

    if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId) ||
        tokenVersionClaim == null || !int.TryParse(tokenVersionClaim, out var tokenVersion))
    {
      context.Fail("Invalid token claims");
      return;
    }

    var user = await unitOfWork.Users.GetByIdAsync(userId);
    if (IsInvalidUser(tokenVersion, user))
    {
      context.Fail("User is not active or token is outdated");
      return;
    }

    var claims = new List<Claim>
    {
      new(CustomClaimTypes.USERNAME, user!.Username),
      new(CustomClaimTypes.EMAIL_VERIFIED, user.EmailVerified.ToString()),
      new(CustomClaimTypes.STATUS, user.Status.ToString())
    };

    var identity = new ClaimsIdentity(claims);
    context.Principal?.AddIdentity(identity);

    await base.TokenValidated(context);
  }

  private static bool IsInvalidUser(int tokenVersion, UserDomain? user)
  {
    return user == null ||
            !user.EmailVerified ||
            user.Status != UserStatus.Active ||
            user.TokenVersion != tokenVersion;
  }
}