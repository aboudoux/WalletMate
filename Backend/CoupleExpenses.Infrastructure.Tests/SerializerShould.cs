using CoupleExpenses.Domain.Periods.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CoupleExpenses.Infrastructure.Tests
{
    public class SerializerShould
    {
        private CustomJsonSerializer _serializer = new CustomJsonSerializer();

        [Fact]
        public void SerializeOperationId()
        {
            var data = _serializer.Serialize(OperationId.From(5));
            data.Should().Be("{\"$type\":\"2fe896e5-89fe-44db-82df-700f5c6d8fa2\",\"Value\":5}");
        }

        [Fact]
        public void DeserializeBadOperationId()
        {
            var json = "{\"$type\":\"2fe896e5-89fe-44db-82df-700f5c6d8fa2\",\"Value\":-50}";
            var operationId = _serializer.Deserialize(json) as OperationId;
            operationId.Value.Should().Be(-50);
        }
    }
}