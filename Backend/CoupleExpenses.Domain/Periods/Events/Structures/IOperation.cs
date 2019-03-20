using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events.Structures
{
    public interface IOperation
    {
        OperationId OperationId { get; }
    }
}