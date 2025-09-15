using FluentValidation;

namespace App.Application.User.Auth.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
  public ResetPasswordCommandValidator()
  {
    RuleFor(x => x.Token)
      .NotEmpty().WithMessage("Reset token is required");

    RuleFor(x => x.NewPassword)
      .NotEmpty().WithMessage("New password is required")
      .MinimumLength(8).WithMessage("New password must be at least 8 characters");

    RuleFor(x => x.ConfirmPassword)
      .Equal(x => x.NewPassword)
      .WithMessage("Confirm password must match new password");
  }
}