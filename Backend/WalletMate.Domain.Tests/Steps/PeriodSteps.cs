using System.Linq;
using FluentAssertions;
using TechTalk.SpecFlow;
using WalletMate.Domain.Periods;
using WalletMate.Domain.Periods.Events;
using WalletMate.Domain.Periods.ValueObjects;
using WalletMate.Domain.Common;

namespace WalletMate.Domain.Tests.Steps {
    [Binding]
    public sealed class PeriodSteps
    {
        private Period _period;

        [Given(@"Une période est créée")]
        public void GivenUnePeriodeEstCreee()
        {
            _period = Period.Create(PeriodName.From(3, 2019));
        }

        [When(@"J'ajoute à la période les dépenses suivantes")]
        [Given(@"j'y ai ajouté les dépenses suivantes")]
        public void WhenJAjouteALaPeriodeLesDepensesSuivantes((Amount amount, Label label, Pair pair, SpendingCategory category)[] operations)
        {
            operations.ForEach(e => _period.AddSpending(e.amount, e.label, e.pair, e.category));
        }

        [When(@"J'ajoute à la période les recettes suivantes")]
        [Given(@"j'y ai ajouté les recettes suivantes")]
        public void WhenJAjouteALaPeriodeLesRecettesSuivantes((Amount amount, Label label, Pair pair, RecipeCategory category)[] operations)
        {
            operations.ForEach(e => _period.AddRecipe(e.amount, e.label, e.pair, e.category));
        }

        [When(@"je modifie le montant de l'operation (.*) en (.*) euros")]
        public void WhenJeModifieLeMontnantLeLOperationEnEuros(OperationId operationId, Amount montant)
        {
            _period.ChangeSpending(operationId, montant);
        }

        [When(@"je modifie le binome de l'operation (.*) en (.*)")]
        public void WhenJeModifieLeBinomeLeLOperationEnMarie(OperationId operationId, Pair binome)
        {
            _period.ChangeSpending(operationId, pair: binome);
        }

        [When(@"je supprime l'opération (.*)")]
        public void WhenJeSupprimeLOperation(OperationId operationId)
        {
            _period.RemoveOperation(operationId);
        }

        [When(@"je modifie le type de l'operation (.*) en (.*)")]
        public void WhenJeModifieLeTypeDeLOperationEnCommun(OperationId operationId,
            SpendingCategory spendingCategory)
        {
            _period.ChangeSpending(operationId, category: spendingCategory);
        }

        [Then(@"le binome (.*) doit la somme de (.*) euros")]
        public void ThenLeBinomeMarieDoitLaSommeDeEuros(Pair binome, Amount montant)
        {
            var balance = _period.UncommittedEvents.GetStream().OfType<PeriodBalanceChanged>().Last();
            balance.AmountDue.Should().Be(montant);
            balance.By.Should().Be(binome);
        }

        [StepArgumentTransformation]
        private static (Amount amount, Label label, Pair pair, SpendingCategory category)[]ToSpendingOperations(Table table)
            => table.Rows.Select(row => (
                    Amount.From(double.Parse(row["Montant"])),
                    Label.From(row["Libelle"]),
                    Pair.From(row["Binome"] == "Aurelien" ? 1 : 2),
                    SpendingCategory.From(row["Categorie"] == "Commun" ? 1 : 2)))
                .ToArray();

        [StepArgumentTransformation]
        private static (Amount amount, Label label, Pair pair, RecipeCategory category)[] ToRecipeOperations(Table table)
            => table.Rows.Select(row => (
                    Amount.From(double.Parse(row["Montant"])),
                    Label.From(row["Libelle"]),
                    Pair.From(row["Binome"] == "Aurelien" ? 1 : 2),
                    RecipeCategory.From(row["Categorie"] == "Commune" ? 1 : 2)))
                .ToArray();
        

        [StepArgumentTransformation]
        private static Pair ToPair(string source)
            => source == "Aurelien"
                ? Pair.First
                : Pair.Second;

        [StepArgumentTransformation]
        private static OperationId ToOperationId(string source)
            => OperationId.From(int.Parse(source));

        [StepArgumentTransformation]
        private static Amount ToAmount(string source)
            => Amount.From(double.Parse(source));

        [StepArgumentTransformation]
        private static SpendingCategory ToSpendingCategory(string source)
            => source == "Commun"
                ? SpendingCategory.Common
                : SpendingCategory.Advance;
    }
}
