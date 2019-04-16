using System;
using WalletMate.Domain.Common.Exceptions;

namespace WalletMate.Domain.Common.ValueObjects.Exceptions
{
    public class EmptyGuidException : WalletMateException {
        public EmptyGuidException(Type valueObjectType)
            : base($"A '{valueObjectType.Name}' cannot be an empty GUID. please, pass a valid guid instead") {
        }       
    }
}