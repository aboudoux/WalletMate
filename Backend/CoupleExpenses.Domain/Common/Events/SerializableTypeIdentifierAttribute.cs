using System;

namespace CoupleExpenses.Domain.Common.Events
{
    public sealed class SerializableTypeIdentifierAttribute : Attribute
    {
        public SerializableTypeIdentifierAttribute(string identifier) => Identifier = identifier;

        public string Identifier { get; }
    }
}