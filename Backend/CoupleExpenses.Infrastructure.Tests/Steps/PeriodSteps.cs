using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoupleExpenses.Application.Periods.Queries;
using CoupleExpenses.Domain.Common;
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
            await UpdateRecipe(periodId, operationId, amount);
        }

        [When(@"je demande à modifier le libellé de la recette numéro (.*) en ""(.*)"" pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeLibelleDeLaRecetteNumeroEnPourLaPeriode(int operationId, string label, string periodId)
        {
            await UpdateRecipe(periodId, operationId, label:label);
        }

        [When(@"je demande à modifier le binôme de la recette numéro (.*) par (.*) pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeBinomeDeLaRecetteNumeroParAurelienPourLaPeriode(int operationId, string newPair, string periodId)
        {
            await UpdateRecipe(periodId, operationId, newPair: newPair);
        }

        [When(@"je demande à modifier le type de la recette numéro (.*) en ""(.*)"" pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeTypeDeLaRecetteNumeroEnPourLaPeriode(int operationId, string newType, string periodId)
        {
            await UpdateRecipe(periodId, operationId, newOperationType:newType);
        }

        [When(@"je demande à modifier le montant de la dépense numéro (.*) en (.*) euros pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeMontantDeLaDepenseNumeroEnEurosPourLaPeriode(int operationId, double amount, string periodId)
        {
            await UpdateSpending(periodId, operationId, amount);
        }

        [When(@"je demande à modifier le libellé de la dépense numéro (.*) en ""(.*)"" pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeLibelleDeLaDepenseNumeroEnPourLaPeriode(int operationId, string label, string periodId)
        {
            await UpdateSpending(periodId, operationId, label:label);
        }

        [When(@"je demande à modifier le binôme de la dépense numéro (.*) par (.*) pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeBinomeDeLaDepenseNumeroParAurelienPourLaPeriode(int operationId, string newPair, string periodId)
        {
            await UpdateSpending(periodId, operationId, newPair: newPair);
        }

        [When(@"je demande à modifier le type de la dépense numéro (.*) en ""(.*)"" pour la période (.*)")]
        public async  Task WhenJeDemandeAModifierLeTypeDeLaDepenseNumeroEnPourLaPeriode(int operationId, string newOperationType, string periodId)
        {
            await UpdateSpending(periodId, operationId, newOperationType: newOperationType);
        }

        [Then(@"L'opération (.*) de la période (.*) à pour type ""(.*)""")]
        public async Task ThenLOperationDeLaPeriodeAPourType(int operationId, string periodId, string operationType)
        {
            await AssertOperation(operationId, periodId, o => o.OperationType.Should().Be(operationType));
        }

        [Then(@"L'opération (.*) de la période (.*) à pour binôme (.*)")]
        public async Task ThenLOperationDeLaPeriodeAPourBinomeAurelien(int operationId, string periodId, string pair)
        {
            await AssertOperation(operationId, periodId, o => o.Pair.Should().Be(pair));
        }

        [Then(@"L'opération (.*) de la période (.*) à un montant de (.*) euros")]
        public async Task ThenLOperationDeLaPeriodeAUnMontantDeEuros(int operationId, string periodId, double amount)
        {
            await AssertOperation(operationId, periodId, o => o.Amount.Should().Be(amount));
        }

        [Then(@"L'opération (.*) de la période (.*) à pour libellé ""(.*)""")]
        public async Task ThenLOperationDeLaPeriodeAPourLibelle(int operationId, string periodId, string label)
        {
            await AssertOperation(operationId, periodId, o => o.Label.Should().Be(label));
        }

        private async Task AssertOperation(int operationId, string periodId, Action<IPeriodOperation> assertion)
        {
            var operations = await FakeServer.GetAllOperations(periodId);
            var recipe = operations.First(a => a.OperationId == operationId);
            assertion(recipe);
        }

        private async Task UpdateRecipe(string periodId, int operationId, double? amount = null, string label = null, string newPair = null, string newOperationType = null)
        {
            var operations = await FakeServer.GetAllOperations(periodId);
            var recipeToChange = operations.First(a => a.OperationId == operationId);
            var result = await FakeServer.ChangeRecipe(
                recipeToChange.PeriodId, 
                recipeToChange.OperationId,
                amount ?? recipeToChange.Amount,
                label ?? recipeToChange.Label,
                TransformPair(newPair ?? recipeToChange.Pair),
                TransformOperationType(newOperationType ?? recipeToChange.OperationType));

            result.Should().Be(HttpStatusCode.OK);

            int TransformPair(string pair) => pair == "Marie" ? 2 : 1;
            int TransformOperationType(string operationType) => operationType == "Commun" ? 1 : 2;
        }

        private async Task UpdateSpending(string periodId, int operationId, double? amount = null, string label = null, string newPair = null, string newOperationType = null)
        {
            var operations = await FakeServer.GetAllOperations(periodId);
            var recipeToChange = operations.First(a => a.OperationId == operationId);
            var result = await FakeServer.ChangeSpending(
                recipeToChange.PeriodId,
                recipeToChange.OperationId,
                amount ?? recipeToChange.Amount,
                label ?? recipeToChange.Label,
                TransformPair(newPair ?? recipeToChange.Pair),
                TransformOperationType(newOperationType ?? recipeToChange.OperationType));

            result.Should().Be(HttpStatusCode.OK);

            int TransformPair(string pair) => pair == "Marie" ? 2 : 1;
            int TransformOperationType(string operationType) => operationType == "Commun" ? 1 : 2;
        }
    }
}
