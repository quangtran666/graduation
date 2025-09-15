using App.Application.Common.Events;

namespace App.Application.User.Auth.Events;

public record PasswordResetRequestedEvent(
  int UserId,
  string Email,
  string Username,
  string ResetToken
) : IEvent
{
  public DateTime OccurredAt { get; } = DateTime.UtcNow;
}