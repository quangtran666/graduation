using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.ResendEmailVerification;

public record ResendEmailVerificationCommand(
  string Email
) : IRequest<ErrorOr<ResendEmailVerificationResult>>;