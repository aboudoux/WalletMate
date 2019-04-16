using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events
{
    [SerializableTypeIdentifier("RecipeRemoved")]
    public sealed class RecipeRemoved : DomainEvent 
    {
        public RecipeRemoved(OperationId operationId) 
        {
            OperationId = operationId;
        }

        public OperationId OperationId { get; }
    }
}