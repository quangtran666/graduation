using FluentValidation;

namespace App.Application.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
  public ForgotPasswordCommandValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email is required")
      .EmailAddress().WithMessage("Email is not valid");
  }
}