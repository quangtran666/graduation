namespace App.Contract.User.Auth.Requests;

public record ResetPasswordRequest(
  string Token,
  string NewPassword,
  string ConfirmPassword
);