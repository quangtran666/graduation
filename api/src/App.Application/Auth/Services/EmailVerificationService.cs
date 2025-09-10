using App.Application.Auth.Configurations;
using App.Application.Auth.Constants;
using App.Application.Auth.Events;
using App.Application.Common.Data;
using App.Domain.Entities;

using ErrorOr;

using MediatR;

using Microsoft.Extensions.Options;

namespace App.Application.Auth.Services;

public interface IEmailVerificationService
{
  Task<ErrorOr<Success>> SendVerificationEmailAsync(User user, CancellationToken cancellationToken = default);
}

public class EmailVerificationService : IEmailVerificationService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly AuthSettings _authSettings;
  private readonly IMediator _mediator;

  public EmailVerificationService(
    IUnitOfWork unitOfWork,
    IOptions<AuthSettings> authSettings,
    IMediator mediator)
  {
    _unitOfWork = unitOfWork;
    _authSettings = authSettings.Value;
    _mediator = mediator;
  }

  public async Task<ErrorOr<Success>> SendVerificationEmailAsync(User user, CancellationToken cancellationToken = default)
  {
    if (user.EmailVerified)
      return Error.Conflict(AuthErrors.User.EMAIL_ALREADY_VERIFIED, "Email is already verified");

    var recentToken = await _unitOfWork.EmailVerificationTokens
      .GetRecentTokenByUserIdAsync(user.Id, TimeSpan.FromSeconds(AuthConstants.EmailVerification.COOLDOWN_SECONDS));

    if (recentToken is not null)
    {
      var remainingSeconds = (int)(AuthConstants.EmailVerification.COOLDOWN_SECONDS - (DateTime.UtcNow - recentToken.CreatedAt).TotalSeconds);
      return Error.Conflict(AuthErrors.EmailVerification.COOLDOWN_ACTIVE,
        $"Please wait {remainingSeconds} seconds before requesting a new verification email.");
    }

    await _unitOfWork.EmailVerificationTokens.InvalidateTokensByUserIdAsync(user.Id);

    var verificationToken = _unitOfWork.EmailVerificationTokens.Create(new()
    {
      UserId = user.Id,
      Token = Guid.NewGuid().ToString(),
      ExpiresAt = DateTime.UtcNow.AddMinutes(_authSettings.EmailVerificationTokenExpirationMinutes),
      UsedAt = null
    });

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    await _mediator.Publish(new UserRegisteredEvent(
      user.Id,
      user.Email,
      user.Username,
      verificationToken.Token
    ), cancellationToken);

    return Result.Success;
  }
}