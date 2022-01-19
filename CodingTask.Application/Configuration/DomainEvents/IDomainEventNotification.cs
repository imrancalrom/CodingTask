using System;
using MediatR;

namespace CodingTask.Application.Configuration.DomainEvents
{
    public interface IDomainEventNotification<out TEventType> : IDomainEventNotification
    {
        TEventType DomainEvent { get; }
    }

    public interface IDomainEventNotification : INotification
    {
        Guid Id { get; }
    }
}