namespace App.Contract.Auth.Responses;

public record VerifyEmailResponse(
  string Message,
  UserInfo User
);