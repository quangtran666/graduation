using FluentValidation;

namespace App.Application.User.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
  public RegisterCommandValidator()
  {
    RuleFor(x => x.Username)
      .NotEmpty().WithMessage("Username is required")
      .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");
    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email is required")
      .EmailAddress().WithMessage("Email is not valid");
    RuleFor(x => x.Password)
      .NotEmpty().WithMessage("Password is required")
      .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    RuleFor(x => x.ConfirmPassword)
      .Equal(x => x.Password)
      .WithMessage("{PropertyName} must be equal to {ComparisonProperty}");
  }
}