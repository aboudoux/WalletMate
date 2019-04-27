using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;
using WalletMate.Application.Periods.Queries;
using WalletMate.Domain.Common;
using WalletMate.Infrastructure.Dto;
using WalletMate.Infrastructure.Tests.Assets;

namespace WalletMate.Infrastructure.Tests.Steps
{
    [Binding]
    public sealed class OperationSteps : StepBase
    {
        public OperationSteps(FakeServer fakeServer, TestContext testContext) : base(fakeServer, testContext)
        {
        }

        [When(@"J'ajoute des dépenses dans l'application")]
        [Given(@"J'ai ajouté des dépenses dans l'application")]
        public async Task WhenJAjouteUneDepenseALaPeriode((string periodId, double amount, string label, string pair, string category)[] spendings)
        {
            await spendings.ForEachAsync(async row =>
            {
                await FakeServer.AddSpending(row.periodId, row.amount, row.label, row.pair, row.category);
            });
        }

        [When(@"J'ajoute des recettes dans l'application")]
        [Given(@"J'ai ajouté des recettes dans l'application")]
        public async Task WhenJApplication((string periodId, double amount, string label, string pair, string category)[] recipe)
        {
            await recipe.ForEachAsync(async row =>
            {
                await FakeServer.AddRecipe(row.periodId, row.amount, row.label, row.pair, row.category);
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

        [When(@"je demande à modifier le montant de la recette numéro (.*) en (.*) euros pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeMontantDeLaRecetteNumeroEnEurosPourLaPeriode(int operationId, double amount, string periodId)
        {
            await UpdateRecipe(periodId, operationId, amount);
        }

        [When(@"je demande à modifier le libellé de la recette numéro (.*) en ""(.*)"" pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeLibelleDeLaRecetteNumeroEnPourLaPeriode(int operationId, string label, string periodId)
        {
            await UpdateRecipe(periodId, operationId, label: label);
        }

        [When(@"je demande à modifier le binôme de la recette numéro (.*) par (.*) pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeBinomeDeLaRecetteNumeroParAurelienPourLaPeriode(int operationId, string newPair, string periodId)
        {
            await UpdateRecipe(periodId, operationId, newPair: newPair);
        }

        [When(@"je demande à modifier le type de la recette numéro (.*) en ""(.*)"" pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeTypeDeLaRecetteNumeroEnPourLaPeriode(int operationId, string newType, string periodId)
        {
            await UpdateRecipe(periodId, operationId, newCategory: newType);
        }

        [When(@"je demande à modifier le montant de la dépense numéro (.*) en (.*) euros pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeMontantDeLaDepenseNumeroEnEurosPourLaPeriode(int operationId, double amount, string periodId)
        {
            await UpdateSpending(periodId, operationId, amount);
        }

        [When(@"je demande à modifier le libellé de la dépense numéro (.*) en ""(.*)"" pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeLibelleDeLaDepenseNumeroEnPourLaPeriode(int operationId, string label, string periodId)
        {
            await UpdateSpending(periodId, operationId, label: label);
        }

        [When(@"je demande à modifier le binôme de la dépense numéro (.*) par (.*) pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeBinomeDeLaDepenseNumeroParAurelienPourLaPeriode(int operationId, string newPair, string periodId)
        {
            await UpdateSpending(periodId, operationId, newPair: newPair);
        }

        [When(@"je demande à modifier le type de la dépense numéro (.*) en ""(.*)"" pour la période (.*)")]
        public async Task WhenJeDemandeAModifierLeTypeDeLaDepenseNumeroEnPourLaPeriode(int operationId, string newCategory, string periodId)
        {
            await UpdateSpending(periodId, operationId, newCategory: newCategory);
        }

        [Then(@"L'opération (.*) de la période (.*) à pour type ""(.*)""")]
        public async Task ThenLOperationDeLaPeriodeAPourType(int operationId, string periodId, string category)
        {
            await AssertOperation(operationId, periodId, o => o.Category.Should().Be(category));
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

        private async Task UpdateRecipe(string periodId, int operationId, double? amount = null, string label = null, string newPair = null, string newCategory = null)
        {
            var operations = await FakeServer.GetAllOperations(periodId);
            var recipeToChange = operations.First(a => a.OperationId == operationId);
            var result = await FakeServer.ChangeRecipe(
                recipeToChange.PeriodId,
                recipeToChange.OperationId,
                amount ?? recipeToChange.Amount,
                label ?? recipeToChange.Label,
                TransformPair(newPair ?? recipeToChange.Pair),
                TransformCategory(newCategory ?? recipeToChange.Category));

            result.Should().Be(HttpStatusCode.OK);

            int TransformPair(string pair) => pair == "Marie" ? 2 : 1;
            int TransformCategory(string category) => category == "Commun" ? 1 : 2;
        }

        private async Task UpdateSpending(string periodId, int operationId, double? amount = null, string label = null, string newPair = null, string newCategory = null)
        {
            var operations = await FakeServer.GetAllOperations(periodId);
            var recipeToChange = operations.First(a => a.OperationId == operationId);
            var result = await FakeServer.ChangeSpending(
                recipeToChange.PeriodId,
                recipeToChange.OperationId,
                amount ?? recipeToChange.Amount,
                label ?? recipeToChange.Label,
                TransformPair(newPair ?? recipeToChange.Pair),
                TransformCategory(newCategory ?? recipeToChange.Category));

            result.Should().Be(HttpStatusCode.OK);

            int TransformPair(string pair) => pair == "Marie" ? 2 : 1;
            int TransformCategory(string category) => category == "Commun" ? 1 : 2;
        }
    }
}