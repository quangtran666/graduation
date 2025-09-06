namespace App.Application.Common.Models;

public record UserData(
    int Id,
    string Username,
    string Email,
    bool EmailVerified
);