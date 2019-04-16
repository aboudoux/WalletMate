using System.Reflection;
using Newtonsoft.Json;
using WalletMate.Domain.Common.Events;

namespace WalletMate.Infrastructure
{
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