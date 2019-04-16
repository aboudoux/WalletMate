using System.Threading.Tasks;
using MediatR;

namespace WalletMate.Domain.Common.Events
{
    public interface IEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }

    public interface IEvent : INotification
    {
    }

    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IEvent
    {
    }
}