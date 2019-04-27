using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;
using WalletMate.Application.Pairs;
using WalletMate.Infrastructure.Tests.Assets;

namespace WalletMate.Infrastructure.Tests.Steps
{
    [Binding]
    public sealed class  PairSteps : StepBase
    {
        private IConfiguredPair _configuredPair;

        public PairSteps(FakeServer fakeServer, TestContext testContext) : base(fakeServer, testContext)
        {
        }

        [When(@"Je demande à obtenir les binômes configurés dans le système")]
        public async Task WhenJeDemandeAObtenirLesBinomesConfiguresDansLeSysteme()
        {
            _configuredPair = await FakeServer.GetConfiguredPair();
        }

        [Then(@"Le premier binôme est ""(.*)"" et le second est ""(.*)""")]
        public void ThenLePremierBinomeEstEtLeSecondEst(string firstPairName, string secondPairName)
        {
            _configuredPair.FirstPairName.Should().Be(firstPairName);
            _configuredPair.SecondPairName.Should().Be(secondPairName);
        }
    }
}