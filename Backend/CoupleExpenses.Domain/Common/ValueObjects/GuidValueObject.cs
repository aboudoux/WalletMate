﻿using System;
using CoupleExpenses.Domain.Common.ValueObjects.Exceptions;

namespace CoupleExpenses.Domain.Common.ValueObjects
{
    public abstract class GuidValueObject<T> : ValueObject<Guid>
        where T : class {
        protected GuidValueObject(Guid value) {
            if (value == Guid.Empty)
                throw new EmptyGuidException(typeof(T));

            Value = value;
        }

        public static T New()
            => From(Guid.NewGuid());

        public static T From(Guid value)
            => CreatePrivateInstance<T>(value);

        public static T From(string value)
            => CreatePrivateInstance<T>(Guid.Parse(value));
    }
}