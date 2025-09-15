using ErrorOr;

using MediatR;

namespace App.Application.User.Auth.Commands.Register;

public record RegisterCommand(
  string Username,
  string Email,
  string Password,
  string ConfirmPassword
) : IRequest<ErrorOr<RegisterResult>>;