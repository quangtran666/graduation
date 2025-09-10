using App.Application.Auth.Configurations;
using App.Application.Auth.Constants;
using App.Application.Auth.Events;
using App.Application.Common.Data;
using App.Domain.Entities;

using ErrorOr;

using MediatR;

using Microsoft.Extensions.Options;

namespace App.Application.Auth.Services;

public class PasswordResetService : IPasswordResetService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMediator _mediator;
  private readonly IPasswordService _passwordService;
  private readonly AuthSettings _authSettings;

  public PasswordResetService(
    IUnitOfWork unitOfWork,
    IMediator mediator,
    IPasswordService passwordService,
    IOptions<AuthSettings> authSettings)
  {
    _unitOfWork = unitOfWork;
    _mediator = mediator;
    _passwordService = passwordService;
    _authSettings = authSettings.Value;
  }

  public async Task<ErrorOr<Success>> SendPasswordResetEmailAsync(User user, CancellationToken cancellationToken = default)
  {
    var recentToken = await _unitOfWork.PasswordResetTokens
      .GetRecentTokenByUserIdAsync(user.Id, TimeSpan.FromSeconds(AuthConstants.PasswordReset.COOLDOWN_SECONDS));

    if (recentToken is not null)
    {
      var remainingSeconds = (int)(AuthConstants.PasswordReset.COOLDOWN_SECONDS - (DateTime.UtcNow - recentToken.CreatedAt).TotalSeconds);
      return Error.Conflict(AuthErrors.PasswordReset.COOLDOWN_ACTIVE,
        $"Please wait {remainingSeconds} seconds before requesting a new password reset email.");
    }

    await _unitOfWork.PasswordResetTokens.InvalidateTokensByUserIdAsync(user.Id);

    var resetToken = _unitOfWork.PasswordResetTokens.Create(new()
    {
      UserId = user.Id,
      Token = Guid.NewGuid().ToString(),
      ExpiresAt = DateTime.UtcNow.AddHours(_authSettings.PasswordResetTokenExpirationHours),
      UsedAt = null
    });

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    await _mediator.Publish(new PasswordResetRequestedEvent(
      user.Id,
      user.Email,
      user.Username,
      resetToken.Token
    ), cancellationToken);

    return Result.Success;
  }

  public async Task<ErrorOr<Success>> ResetPasswordAsync(string token, string newPassword, CancellationToken cancellationToken = default)
  {
    var resetToken = await _unitOfWork.PasswordResetTokens.GetByTokenAsync(token);

    if (resetToken is null)
      return Error.NotFound(AuthErrors.PasswordReset.TOKEN_NOT_FOUND, "Invalid or expired token");

    if (resetToken.UsedAt.HasValue)
      return Error.Conflict(AuthErrors.PasswordReset.TOKEN_ALREADY_USED, "Token has already been used");

    if (resetToken.ExpiresAt < DateTime.UtcNow)
      return Error.Conflict(AuthErrors.PasswordReset.TOKEN_EXPIRED, "Token has expired");

    var user = await _unitOfWork.Users.GetByIdAsync(resetToken.UserId);
    if (user is null)
      return Error.NotFound(AuthErrors.User.NOT_FOUND, "User not found");

    user.PasswordHash = _passwordService.HashPassword(newPassword);
    user.TokenVersion++; // Invalidate all existing tokens

    resetToken.UsedAt = DateTime.UtcNow;
    _unitOfWork.PasswordResetTokens.Update(resetToken);

    await _unitOfWork.PasswordResetTokens.InvalidateTokensByUserIdAsync(user.Id);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    await _mediator.Publish(new PasswordResetCompletedEvent(
      user.Id,
      user.Email,
      user.Username
    ), cancellationToken);

    return Result.Success;
  }
}