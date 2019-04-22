using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WalletMate.Domain.Periods.ValueObjects;
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
                    new PeriodOperation(e.periodId, e.operationId,e.type, e.pair, e.pair.ToPairValue(), e.amount, e.label, e.category, e.category.ToCategoryValue()))
                .ToArray();

        [StepArgumentTransformation]
        public static PeriodResult[] ToPeriodResults(Table table)
            => table.CreateSet<(string PeriodId, string PeriodName)>()
                .Select(e => new PeriodResult(e.PeriodName, e.PeriodId))
                .ToArray();
    }

    public static class ValueTransformation
    {
        public static int ToPairValue(this string pair)
            => pair == "Aurélien" ? 1 : 2;

        public static int ToCategoryValue(this string category)
            => category == "Commun" ? 1 : 2;
    }
}