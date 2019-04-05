using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Periods.ValueObjects;
using CoupleExpenses.Infrastructure.Dto;
using CoupleExpenses.Infrastructure.Tests.Assets;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CoupleExpenses.Infrastructure.Tests.Steps
{
    [Binding]
    public sealed class PeriodSteps : StepBase
    {
        public PeriodSteps(FakeServer fakeServer, TestContext testContext) : base(fakeServer, testContext)
        {
        }

        [When(@"Je demande la création d'une période pour le mois (.*) et l'année (.*)")]
        [Given(@"J'ai demandé la création d'une période pour le mois (.*) et l'année (.*)")]
        public async Task WhenJeDemandeLaCreationDeLaPeriodePourLeMoisEtLAnnee(int month, int year)
        {
            try
            {
                await FakeServer.CreatePeriod(month, year);
            }
            catch (HttpServerError e)
            {
                TestContext.AddError(e);
            }
        }

        [Then(@"La liste des périodes contient ""(.*)""")]
        public async Task ThenLaListeDesPeriodesContient(string expectedPeriodName)
        {
            var periods = await FakeServer.GetAllPeriod();
            periods.Should().Contain(expectedPeriodName);
        }

        [When(@"J'ajoute des dépenses dans l'application")]
        [Given(@"J'ai ajouté des dépenses dans l'application")]
        public async Task WhenJAjouteUneDepenseALaPeriode((string periode, double montant, string libelle, string binome, string typeOperation)[] spendings)
        {
            await spendings.ForEachAsync(async row =>
            {
                await FakeServer.AddSpending(row.periode, row.montant, row.libelle, row.binome, row.typeOperation);
            });
        }

        [When(@"J'ajoute des recettes dans l'application")]
        [Given(@"J'ai ajouté des recettes dans l'application")]
        public async Task WhenJApplication((string periode, double montant, string libelle, string binome, string typeOperation)[] recipe)
        {
            await recipe.ForEachAsync(async row =>
            {
                await FakeServer.AddRecipe(row.periode, row.montant, row.libelle, row.binome, row.typeOperation);
            });
        }

        [Then(@"La liste des opérations pour la période (.*) contient les elements suivants")]
        [Given(@"La liste des opérations pour la période (.*) contient les elements suivants")]
        public async Task ThenLaListeDesOperationsPourLaPeriodeContientLesElementsSuivants(string periodId, PeriodOperation[] expectedOperations)
        {
            var operations = await FakeServer.GetAllOperations(periodId);

            operations.Should().NotBeEmpty();
            expectedOperations.ForEach(e => operations.Should().ContainEquivalentOf(e));
        }

        [When(@"Je demande à supprimer l'opération (.*) de la période (.*)")]
        public async Task WhenJeDemandeASupprimerLOperationDeLaPeriode(int operationId, string periodId)
        {
            await FakeServer.RemoveOperation(periodId, operationId);
        }

        [Then(@"La liste des opérations pour la période (.*) est vide")]
        public async Task ThenLaListeDesOperationsPourLaPeriodeEstVide(string periodId)
        {
            var operations = await FakeServer.GetAllOperations(periodId);
            operations.Should().BeEmpty();
        }

        [Then(@"(.*) doit la somme de (.*) euros pour la période (.*)")]
        public async Task ThenAurelienDoitLaSommeDeEurosPourLaPeriode(string pair, double amount, string periodId)
        {
            var balance = await FakeServer.GetPeriodBalance(periodId);
            balance.Should().NotBe(null);
            balance.AmountDue.Should().Be(amount);
            balance.By.Should().Be(pair);
        }

        [When(@"je demande à modifier le montant de la recette numéro (.*) en (.*) euros pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeMontantDeLaRecetteNumeroEnEurosPourLaPeriode(int operationId, double amount, string periodId)
        {
            var operations = await FakeServer.GetAllOperations(periodId);
            var recipeToChange = operations.First(a => a.OperationId == operationId);
            var result = await FakeServer.ChangeRecipe(recipeToChange.PeriodId, recipeToChange.OperationId, amount, recipeToChange.Label, TransformPair(recipeToChange.Pair), TransformOperationType(recipeToChange.OperationType));
            result.Should().Be(HttpStatusCode.OK);

            int TransformPair(string pair) => pair == "Marie" ? 2 : 1;
            int TransformOperationType(string operationType) => operationType == "Commun" ? 1 : 2;
        }

        [Then(@"L'opération (.*) de la période (.*) à un montant de (.*) euros")]
        public async Task ThenLOperationDeLaPeriodeAUnMontantDeEuros(int operationId, string periodId, double amount)
        {
            var operations = await FakeServer.GetAllOperations(periodId);
            var recipe = operations.First(a => a.OperationId == operationId);
            recipe.Amount.Should().Be(amount);
        }
    }
}
