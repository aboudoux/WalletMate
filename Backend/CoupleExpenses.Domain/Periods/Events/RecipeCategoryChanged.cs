using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events
{
    [SerializableTypeIdentifier("RecipeCategoryChanged")]
    public sealed class RecipeCategoryChanged : DomainEvent 
    {
        public RecipeCategoryChanged(OperationId operationId, RecipeCategory category) {
            OperationId = operationId;
            Category = category;
        }

        public OperationId OperationId { get; }
        public RecipeCategory Category { get; }
    }
}