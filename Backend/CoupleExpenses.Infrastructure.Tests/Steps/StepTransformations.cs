using System.Linq;
using CoupleExpenses.Infrastructure.Dto;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CoupleExpenses.Infrastructure.Tests.Steps
{
    [Binding]
    public sealed class StepTransformations
    {
        [StepArgumentTransformation]
        public static (string periode, double montant, string libelle, string binome, string typeOperation)[] ToOperationInput(Table table)
            => table
                .CreateSet<(string periode, double montant, string libelle, string binome, string typeOperation)>()
                .ToArray();

        [StepArgumentTransformation]
        public static PeriodOperation[] ToPeriodOperations(Table table)
            => table
                .CreateSet<(string type, int operationId, string periode, double montant, string libelle, string binome, string typeOperation)>()
                .Select(e =>
                    new PeriodOperation(e.periode, e.operationId,e.type, e.binome, e.montant, e.libelle, e.typeOperation))
                .ToArray();
    }
}