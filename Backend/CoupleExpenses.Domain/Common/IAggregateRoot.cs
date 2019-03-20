using CoupleExpenses.Domain.Common.Events;

namespace CoupleExpenses.Domain.Common
{
    public interface IAggregateRoot
    {
        UncommittedEvents UncommittedEvents { get; }
    }
}