using App.Application.Common.Models;

namespace App.Application.Auth.Queries.GetCurrentUser;

public record GetCurrentUserResult(
    string Message,
    UserData User
);