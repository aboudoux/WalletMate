using System;
using CoupleExpenses.Domain.Common.Exceptions;

namespace CoupleExpenses.Domain.Common.ValueObjects.Exceptions
{
    public class EmptyGuidException : CoupleExpensesException {
        public EmptyGuidException(Type valueObjectType)
            : base($"A '{valueObjectType.Name}' cannot be an empty GUID. please, pass a valid guid instead") {
        }       
    }
}