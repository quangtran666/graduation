using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.VerifyEmail;

public record VerifyEmailCommand(string Token) : IRequest<ErrorOr<VerifyEmailResult>>;