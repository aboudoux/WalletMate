using System.Linq;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Periods;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CoupleExpenses.Domain.Tests.Steps {
    [Binding]
    public sealed class PeriodSteps
    {
        private Period _period;

        [Given(@"Une période est créée")]
        public void GivenUnePeriodeEstCreee() 
        {
            _period = Period.Create(PeriodName.From(3,2019));
        }

        [When(@"J'ajoute à la période les dépenses suivantes")]
        public void WhenJAjouteALaPeriodeLesDepensesSuivantes((Amount amount, Label label, Pair pair, SpendingOperationType operationType)[] operations) 
        {
            operations.ForEach(e => _period.AddSpending(e.amount, e.label, e.pair, e.operationType));
        }

        [When(@"J'ajoute à la période les recettes suivantes")]
        public void WhenJAjouteALaPeriodeLesRecettesSuivantes((Amount amount, Label label, Pair pair, RecipeOperationType operationType)[] operations)
        {
            operations.ForEach(e => _period.AddRecipe(e.amount, e.label, e.pair, e.operationType));
        }

        [Then(@"le binome (.*) doit la somme de (.*) euros")]
        public void ThenLeBinomeMarieDoitLaSommeDeEuros(PairInfo binome, double montant)
        {
            var balance = _period.UncommittedEvents.GetStream().OfType<PeriodBalanceChanged>().Last();
            balance.AmountDue.Should().Be(montant);
            balance.By.Should().Be(binome);
        }

        [StepArgumentTransformation]
        private static (Amount amount, Label label, Pair pair, SpendingOperationType operationType)[] ToSpendingOperations(
            Table table)
            => table.Rows.Select(row => (
                    Amount.From(double.Parse(row["Montant"])),
                    Label.From(row["Libelle"]),
                    Pair.From(row["Binome"] == "Aurelien" ? 1 : 2),
                    SpendingOperationType.From(row["Type"] == "Commun" ? 1 : 2)))
                .ToArray();

        [StepArgumentTransformation]
        private static (Amount amount, Label label, Pair pair, RecipeOperationType operationType)[] ToRecipeOperations(
            Table table)
            => table.Rows.Select(row => (
                    Amount.From(double.Parse(row["Montant"])),
                    Label.From(row["Libelle"]),
                    Pair.From(row["Binome"] == "Aurelien" ? 1 : 2),
                    RecipeOperationType.From(row["Type"] == "Partielle" ? 1 : 2)))
                .ToArray();

        [StepArgumentTransformation]
        private static PairInfo ToPair(string source)
            => source == "Aurelien" 
                ? PairInfo.Aurelien 
                : PairInfo.Marie;
    }
}
