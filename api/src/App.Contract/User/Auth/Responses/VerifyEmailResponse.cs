namespace App.Contract.User.Auth.Responses;

public record VerifyEmailResponse(
  string Message,
  UserInfo User
);