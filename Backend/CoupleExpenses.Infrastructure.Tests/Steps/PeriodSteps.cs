using System.Linq;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Periods.ValueObjects;
using CoupleExpenses.Infrastructure.Dto;
using CoupleExpenses.Infrastructure.Tests.Assets;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
        public async Task WhenJAjouteUneDepenseALaPeriode((string type, string periode, double montant, string libelle, string binome, string typeOperation)[] spendings)
        {
            await spendings.ForEachAsync(async row =>
            {
                await FakeServer.AddSpending(row.periode, row.montant, row.libelle, row.binome, row.typeOperation);
            });
        }

        [When(@"J'ajoute des recettes dans l'application")]
        public async Task WhenJApplication((string type, string periode, double montant, string libelle, string binome, string typeOperation)[] recipe)
        {
            await recipe.ForEachAsync(async row =>
            {
                await FakeServer.AddRecipe(row.periode, row.montant, row.libelle, row.binome, row.typeOperation);
            });
        }

        [Then(@"La liste des opérations pour la période (.*) contient les elements suivants")]
        public async Task ThenLaListeDesOperationsPourLaPeriodeContientLesElementsSuivants(PeriodId periodId, PeriodOperation[] expectedOperations)
        {
            var operations = await FakeServer.GetAllOperations(periodId.ToString());

            operations.Should().NotBeEmpty();
            expectedOperations.ForEach(e => operations.Should().ContainEquivalentOf(e));
        }
    }

    [Binding]
    public sealed class StepTransformations
    {
        [StepArgumentTransformation]
        public static PeriodId ToPeriodId(string value)
            => PeriodId.From(value);

        [StepArgumentTransformation]
        public static (string type, string periode, double montant, string libelle, string binome, string typeOperation)[] ToFlatOperation(Table table)
            => table.CreateSet<(string type, string periode, double montant, string libelle, string binome, string typeOperation)>()
                .ToArray();

        [StepArgumentTransformation]
        public static PeriodOperation[] ToPeriodOperations(Table table)
            => ToFlatOperation(table).Select(row =>
                new PeriodOperation(row.periode, row.type, row.binome, row.montant, row.libelle, row.typeOperation)).ToArray();
    }
}
