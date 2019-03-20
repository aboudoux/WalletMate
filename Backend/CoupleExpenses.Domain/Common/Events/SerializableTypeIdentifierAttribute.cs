using System;

namespace CoupleExpenses.Domain.Common.Events
{
    public class SerializableTypeIdentifierAttribute : Attribute 
    {
        public SerializableTypeIdentifierAttribute(string identifier) => Identifier = Guid.Parse(identifier);

        public virtual Guid Identifier { get; }
    }
}