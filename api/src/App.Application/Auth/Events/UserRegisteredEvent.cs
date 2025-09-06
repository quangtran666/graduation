using App.Application.Common.Events;

namespace App.Application.Auth.Events;

public record UserRegisteredEvent(
  int UserId,
  string Email,
  string Username,
  string VerificationToken
) : IEvent
{
  public DateTime OccurredAt { get; } = DateTime.UtcNow;
}