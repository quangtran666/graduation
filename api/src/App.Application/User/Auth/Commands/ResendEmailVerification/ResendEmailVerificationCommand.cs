using ErrorOr;

using MediatR;

namespace App.Application.User.Auth.Commands.ResendEmailVerification;

public record ResendEmailVerificationCommand(
  string Email
) : IRequest<ErrorOr<ResendEmailVerificationResult>>;