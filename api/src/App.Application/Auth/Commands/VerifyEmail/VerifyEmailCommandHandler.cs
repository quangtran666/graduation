
using App.Application.Auth.Constants;
using App.Application.Common.Data;

using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.VerifyEmail;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, ErrorOr<VerifyEmailResult>>
{
  private readonly IUnitOfWork _unitOfWork;

  public VerifyEmailCommandHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<ErrorOr<VerifyEmailResult>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
  {
    var token = await _unitOfWork.EmailVerificationTokens.GetByTokenAsync(request.Token);
    if (token is null)
      return Error.NotFound(AuthErrors.EmailVerification.TOKEN_NOT_FOUND, "Verification token not found");

    if (token.ExpiresAt < DateTime.UtcNow)
      return Error.Validation(AuthErrors.EmailVerification.TOKEN_EXPIRED, "Verification token has expired");

    if (token.UsedAt is not null)
      return Error.Conflict(AuthErrors.EmailVerification.TOKEN_ALREADY_USED, "Verification token has already been used");

    var user = await _unitOfWork.Users.GetByIdAsync(token.UserId);
    if (user is null)
      return Error.NotFound(AuthErrors.User.NOT_FOUND, "User not found");

    if (user.EmailVerified)
      return Error.Conflict(AuthErrors.User.EMAIL_ALREADY_VERIFIED, "Email is already verified");

    user.EmailVerified = true;
    token.UsedAt = DateTime.UtcNow;

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new VerifyEmailResult(
      Message: "Email verified successfully",
      User: new(user.Id, user.Username, user.Email, user.EmailVerified)
    );
  }
}