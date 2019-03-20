using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common.Events;
using Newtonsoft.Json;

namespace CoupleExpenses.Infrastructure 
{
    public sealed class FileEventStore : IEventStore
    {
        private readonly ISerializer _serializer;
        private string _eventStoreFileName = "CoupleExpensesData.csv";
        public FileEventStore(ISerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

            if (!File.Exists(_eventStoreFileName))
                File.Create(_eventStoreFileName);
        }

        public Task<IDomainEvent[]> GetEvents(Predicate<IDomainEvent> predicate)
        {
            var domainEvents = File.ReadLines(_eventStoreFileName)
                .Select(line => _serializer.Deserialize(line) as IDomainEvent)
                .Where(@event => predicate(@event))
                .ToArray();
                
            return Task.FromResult(domainEvents);
        }

        public Task Save(IDomainEvent @event)
        {
            File.AppendAllText(_eventStoreFileName, _serializer.Serialize(@event) + Environment.NewLine);                            
            return Task.CompletedTask;
        }

        public Task<int> GetLastSequence(string aggregateId)
        {
            throw new NotImplementedException();
        }
    }

    public interface ISerializer
    {
        string Serialize(object value);
        object Deserialize(string value);
    }

    public class CustomJsonSerializer: ISerializer
    {
        private readonly JsonSerializerSettings _settings;

        public CustomJsonSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
                DateParseHandling = DateParseHandling.DateTimeOffset,                
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                SerializationBinder = new SerializableTypeBinder(Assembly.GetAssembly(typeof(IDomainEvent)))
            };
        }

        public string Serialize(object value) => JsonConvert.SerializeObject(value, _settings);
        public object Deserialize(string value) => JsonConvert.DeserializeObject(value, _settings);
    }
}
