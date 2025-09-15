using ErrorOr;

using MediatR;

namespace App.Application.User.Auth.Commands.VerifyEmail;

public record VerifyEmailCommand(string Token) : IRequest<ErrorOr<VerifyEmailResult>>;