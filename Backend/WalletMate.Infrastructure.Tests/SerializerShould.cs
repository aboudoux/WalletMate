using FluentAssertions;
using WalletMate.Domain.Periods.ValueObjects;
using Xunit;

namespace WalletMate.Infrastructure.WebAppTests
{
    public class SerializerShould
    {
        private readonly CustomJsonSerializer _serializer = new CustomJsonSerializer();

        [Fact]
        public void SerializeOperationId()
        {
            var data = _serializer.Serialize(OperationId.From(5));
            data.Should().Be("{\"$type\":\"OperationId\",\"Value\":5}");
        }

        [Fact]
        public void DeserializeBadOperationId()
        {
            var json = "{\"$type\":\"OperationId\",\"Value\":-50}";
            var operationId = _serializer.Deserialize(json) as OperationId;
            operationId.Value.Should().Be(-50);
        }        
    }
}