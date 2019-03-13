using System;
using CoupleExpenses.Domain.Common.Exceptions;

namespace CoupleExpenses.Domain.Common.ValueObjects.Exceptions
{
    [Serializable]
    public class NegativeNumberException : CoupleExpensesException
    {
        public NegativeNumberException(Type valueObjectType)
            : base($"A {valueObjectType.Name} cannot be a negative value")
        {            
        }
    }
}