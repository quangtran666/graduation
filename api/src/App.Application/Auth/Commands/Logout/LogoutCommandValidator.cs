using FluentValidation;

namespace App.Application.Auth.Commands.Logout;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
  public LogoutCommandValidator()
  {
    RuleFor(x => x.RefreshToken)
      .NotEmpty().WithMessage("Refresh token is required");
  }
}