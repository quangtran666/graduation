using FluentValidation;

namespace App.Application.Auth.Commands.RefreshTokens;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
  public RefreshTokenCommandValidator()
  {
    RuleFor(x => x.RefreshToken)
      .NotEmpty().WithMessage("Refresh token is required");
  }
}