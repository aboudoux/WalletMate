using CoupleExpenses.Domain.Common.Events;

namespace CoupleExpenses.Domain.Periods.Events
{
    public sealed class RecipeRemoved : DomainEvent {
        public RecipeRemoved(int operationId) 
        {
            OperationId = operationId;
        }

        public int OperationId { get; }
    }
}