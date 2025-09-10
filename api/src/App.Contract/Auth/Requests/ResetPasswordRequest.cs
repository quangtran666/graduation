namespace App.Contract.Auth.Requests;

public record ResetPasswordRequest(
  string Token,
  string NewPassword,
  string ConfirmPassword
);