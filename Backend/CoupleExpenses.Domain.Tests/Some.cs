using System;
using System.Collections.Generic;
using System.Linq;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Tests
{
    public static class Some
    {
        public static Period Period(Action<EventStack> events = null)
        {
            var periodName = PeriodName.From(3, 2019);
            if (events == null)
                return Periods.Period.Create(periodName);

            var periodCreated = new PeriodCreated(periodName);
            
            var eventStack = new EventStack();
            events.Invoke(eventStack);

            var history = new History( new List<IDomainEvent>{ periodCreated }.Concat(eventStack.All()).ToList());
            return new Period(history);
        }

        public static PeriodCreator PeriodCreator(Action<EventStack> events = null)
        {
            if(events == null)
                return new PeriodCreator(History.Empty);

            var eventStack = new EventStack();
            events.Invoke(eventStack);
            return new PeriodCreator(new History( eventStack.All() ));
        }

        public static SpendingAdded SpendingAdded(OperationId operationId) 
            => new SpendingAdded(operationId.Value, 10, "TEST", PairInfo.Aurelien, SpendingOperationTypeInfo.Common);

        public static RecipeAdded RecipeAdded(OperationId operationId)
            => new RecipeAdded(operationId.Value, 10, "TEST", PairInfo.Aurelien, RecipeOperationTypeInfo.Common);

        public static PeriodCreated PeriodCreated(PeriodName periodName) => new PeriodCreated(periodName);
    }
    
    public sealed class EventStack
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

        public EventStack WithEvent(IDomainEvent @event)
        {
            _events.Add(@event);
            return this;
        }

        public IReadOnlyList<IDomainEvent> All() => _events;
    }
}