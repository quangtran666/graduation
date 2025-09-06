using MediatR;

namespace App.Application.Common.Events;

public interface IEvent : INotification
{
  DateTime OccurredAt { get; }
}