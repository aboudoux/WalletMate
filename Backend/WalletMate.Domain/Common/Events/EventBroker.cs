﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletMate.Domain.Common.Exceptions;

namespace WalletMate.Domain.Common.Events
{
    public class EventBroker : IEventBroker 
    {
        private readonly IEventStore _eventStore;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IUserService _userService;

        public EventBroker(IEventStore eventStore, IEventDispatcher eventDispatcher, IUserService userService) 
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task Publish(UncommittedAggregates aggregates) 
            => await aggregates.GetAll().ForEachAsync(async a => await Publish(a.UncommittedEvents));

        public async Task Publish(UncommittedEvents events) 
        {
            foreach (var @event in events.GetStream()) {
                Stamp(@event);
                await CheckSequence(@event);
                await SaveAndDispatch(@event);
            }

            async Task CheckSequence(IDomainEvent @event) {
                if (await _eventStore.GetLastSequence(@event.AggregateId) >= @event.Sequence) {
                    throw new ConcurrencyException();
                }
            }

            async Task SaveAndDispatch(IDomainEvent @event) {
                await _eventStore.Save(@event);
                await _eventDispatcher.Dispatch(@event);
            }

            void Stamp(IDomainEvent @event) 
                => ((IEventMetaData)@event).SetCreationInfos(_userService.GetCurrentUserName(), DateTimeOffset.Now);
        }

        public async Task<T> GetAggregate<T>(string aggregateId)
        {
            return await GetAggregate<T>(a => a.AggregateId == aggregateId);
        }

        public async Task<T> GetAggregate<T>(Predicate<IDomainEvent> eventSelector)
        {
            var events = await _eventStore.GetEvents(eventSelector);
            return CreateAggregateFromEvents<T>(events);
        }

        private T CreateAggregateFromEvents<T>(IReadOnlyList<IDomainEvent> events)
        {
            if (events == default || !events.Any()) {
                throw new AggregateNotFoundException(typeof(T));
            }

            var history = new History(events);
            var aggregate = (T) Activator.CreateInstance(typeof(T), history);
            return aggregate;
        }
    }
}