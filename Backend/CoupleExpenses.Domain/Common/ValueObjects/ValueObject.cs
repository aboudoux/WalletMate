using System;
using System.Collections.Generic;
using System.Reflection;
using CoupleExpenses.Domain.Common.Exceptions;

namespace CoupleExpenses.Domain.Common.ValueObjects {
    public abstract class ValueObject<TValue> : IEquatable<ValueObject<TValue>>, IValueObject {
        public TValue Value { get; protected set; }

        protected static T CreatePrivateInstance<T>(object param) where T : class {
            try {
                return (T) Activator.CreateInstance(typeof(T), BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { param }, null);
            }
            catch (Exception e) {
                throw e.LastException();
            }
        }

        public override string ToString()
            => Value.ToString();

        public virtual bool Equals(ValueObject<TValue> other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValueObject<TValue>) obj);
        }

        public override int GetHashCode() {
            return EqualityComparer<TValue>.Default.GetHashCode(Value);
        }

        public static bool operator ==(ValueObject<TValue> value1, ValueObject<TValue> value2) {
            return value1?.Equals(value2) ?? Equals(value2, default);
        }

        public static bool operator !=(ValueObject<TValue> value1, ValueObject<TValue> value2) {
            return !value1?.Equals(value2) ?? !Equals(value2, default);
        }
    }

    public abstract class ValueObject<TValue1, TValue2> : IValueObject, IEquatable<ValueObject<TValue1, TValue2>> {

        public TValue1 Value1 { get; protected set; }
        public TValue2 Value2 { get; protected set; }

        protected static T CreatePrivateInstance<T>(object param1, object param2) where T : class {
            try {
                return (T) Activator.CreateInstance(typeof(T), BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { param1, param2 }, null);
            }
            catch (Exception e) {
                throw e.LastException();
            }
        }

        public bool Equals(ValueObject<TValue1, TValue2> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TValue1>.Default.Equals(Value1, other.Value1) && EqualityComparer<TValue2>.Default.Equals(Value2, other.Value2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValueObject<TValue1, TValue2>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TValue1>.Default.GetHashCode(Value1) * 397) ^ EqualityComparer<TValue2>.Default.GetHashCode(Value2);
            }
        }

        public static bool operator ==(ValueObject<TValue1, TValue2> value1, ValueObject<TValue1, TValue2> value2) {
            return value1?.Equals(value2) ?? Equals(value2, default);
        }

        public static bool operator !=(ValueObject<TValue1, TValue2> value1, ValueObject<TValue1, TValue2> value2) {
            return !value1?.Equals(value2) ?? !Equals(value2, default);
        }
    }
}
