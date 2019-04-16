using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WalletMate.Infrastructure.Dto;

namespace WalletMate.Infrastructure.Tests.Steps
{
    [Binding]
    public sealed class StepTransformations
    {
        [StepArgumentTransformation]
        public static (string periodeId, double amount, string label, string pair, string category)[] ToOperationInput(Table table)
            => table
                .CreateSet<(string periodeId, double amount, string label, string pair, string category)>()
                .ToArray();

        [StepArgumentTransformation]
        public static PeriodOperation[] ToPeriodOperations(Table table)
            => table
                .CreateSet<(string type, int operationId, string periodId, double amount, string label, string pair, string category)>()
                .Select(e =>
                    new PeriodOperation(e.periodId, e.operationId,e.type, e.pair, e.amount, e.label, e.category))
                .ToArray();
    }
}