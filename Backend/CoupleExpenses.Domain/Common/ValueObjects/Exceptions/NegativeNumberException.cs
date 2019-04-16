﻿using System;
using WalletMate.Domain.Common.Exceptions;

namespace WalletMate.Domain.Common.ValueObjects.Exceptions
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