using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events.Structures
{
    public interface IOperation
    {
        OperationId OperationId { get; }
    }
}