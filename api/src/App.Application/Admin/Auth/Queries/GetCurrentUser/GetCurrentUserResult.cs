using App.Application.Common.Models;

namespace App.Application.Admin.Auth.Queries.GetCurrentUser;

public record GetCurrentUserResult(
    string Message,
    UserData User
);