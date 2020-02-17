using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;
using WalletMate.Infrastructure.Dto;
using WalletMate.Infrastructure.WebAppTests.Assets;

namespace WalletMate.Infrastructure.WebAppTests.Steps
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

        [Then(@"La liste des périodes contient ""(.*)"" avec l'identifiant (.*)")]
        public async Task ThenLaListeDesPeriodesContient(string expectedPeriodName, string expectedPeriodId)
        {
            var periods = await FakeServer.GetAllPeriod();
            periods.Should().ContainEquivalentOf(new PeriodResult(expectedPeriodName, expectedPeriodId));
        }
       
        [Then(@"(.*) doit la somme de (.*) euros pour la période (.*)")]
        public async Task ThenAurelienDoitLaSommeDeEurosPourLaPeriode(string pair, double amount, string periodId)
        {
            var balance = await FakeServer.GetPeriodBalance(periodId);
            balance.Should().NotBe(null);
            balance.AmountDue.Should().Be(amount);
            balance.By.Should().Be(pair);
        }

        [Then(@"la liste des périodes est présentée dans l'ordre décroissant")]
        public async  Task ThenLaListeDesPeriodesEstPresenteeDansCetOrdre()
        {
            var periods = await FakeServer.GetAllPeriod();
            periods.Should().BeInDescendingOrder(a => a.PeriodId);
        }
    }
}
