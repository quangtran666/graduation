using FluentValidation;

namespace App.Application.User.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
  public LoginCommandValidator()
  {
    RuleFor(x => x.UsernameOrEmail)
      .NotEmpty().WithMessage("Username or email is required");

    RuleFor(x => x.Password)
      .NotEmpty().WithMessage("Password is required");
  }
}