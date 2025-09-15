using App.Application.Common.Data;
using App.Application.User.Auth.Constants;
using App.Application.User.Auth.Services;

using ErrorOr;

using MediatR;

namespace App.Application.User.Auth.Commands.ResendEmailVerification;

public class ResendEmailVerificationCommandHandler : IRequestHandler<ResendEmailVerificationCommand, ErrorOr<ResendEmailVerificationResult>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IEmailVerificationService _emailVerificationService;

  public ResendEmailVerificationCommandHandler(
    IUnitOfWork unitOfWork,
    IEmailVerificationService emailVerificationService)
  {
    _unitOfWork = unitOfWork;
    _emailVerificationService = emailVerificationService;
  }

  public async Task<ErrorOr<ResendEmailVerificationResult>> Handle(ResendEmailVerificationCommand request, CancellationToken cancellationToken)
  {
    var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
    if (user is null)
      return Error.NotFound(AuthErrors.User.NOT_FOUND, "User not found");

    var emailResult = await _emailVerificationService.SendVerificationEmailAsync(user, cancellationToken);

    return emailResult.MatchFirst<ErrorOr<ResendEmailVerificationResult>>(
      success => new ResendEmailVerificationResult(
        Message: "Verification email resent successfully.",
        CooldownSeconds: AuthConstants.EmailVerification.COOLDOWN_SECONDS
      ),
      error => error
    );
  }
}