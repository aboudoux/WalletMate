using System;
using System.Threading.Tasks;
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
    }
}
