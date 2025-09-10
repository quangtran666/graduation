using App.Application.Common.Events;

namespace App.Application.Auth.Events;

public record PasswordResetCompletedEvent(
  int UserId,
  string Email,
  string Username
) : IEvent
{
  public DateTime OccurredAt { get; } = DateTime.UtcNow;
}