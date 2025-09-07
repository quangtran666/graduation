
using App.Application.Auth.Configurations;
using App.Application.Auth.Constants;
using App.Application.Auth.Events;
using App.Application.Common.Data;

using ErrorOr;

using MediatR;

using Microsoft.Extensions.Options;

namespace App.Application.Auth.Commands.ResendEmailVerification;

public class ResendEmailVerificationCommandHandler : IRequestHandler<ResendEmailVerificationCommand, ErrorOr<ResendEmailVerificationResult>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly AuthSettings _authSettings;
  private readonly IMediator _mediator;
  private const int COOLDOWN_SECONDS = 60;

  public ResendEmailVerificationCommandHandler(
    IUnitOfWork unitOfWork,
    IOptions<AuthSettings> authSettings,
    IMediator mediator)
  {
    _unitOfWork = unitOfWork;
    _authSettings = authSettings.Value;
    _mediator = mediator;
  }

  public async Task<ErrorOr<ResendEmailVerificationResult>> Handle(ResendEmailVerificationCommand request, CancellationToken cancellationToken)
  {
    var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
    if (user is null)
      return Error.NotFound(AuthErrors.User.NOT_FOUND, "User not found");

    if (user.EmailVerified)
      return Error.Conflict(AuthErrors.User.EMAIL_ALREADY_VERIFIED, "Email is already verified");

    var recentToken = await _unitOfWork.EmailVerificationTokens
      .GetRecentTokenByUserIdAsync(user.Id, TimeSpan.FromSeconds(COOLDOWN_SECONDS));

    if (recentToken is not null)
    {
      var remainingSeconds = (int)(COOLDOWN_SECONDS - (DateTime.UtcNow - recentToken.CreatedAt).TotalSeconds);
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

    return new ResendEmailVerificationResult(
      Message: "Verification email resent successfully.",
      CooldownSeconds: COOLDOWN_SECONDS
    );
  }
}