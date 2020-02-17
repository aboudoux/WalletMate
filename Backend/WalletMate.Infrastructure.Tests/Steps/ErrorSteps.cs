using FluentAssertions;
using TechTalk.SpecFlow;
using WalletMate.Infrastructure.WebAppTests.Assets;

namespace WalletMate.Infrastructure.WebAppTests.Steps
{
    [Binding]
    public sealed class ErrorSteps : StepBase
    {
        public ErrorSteps(FakeServer fakeServer, TestContext testContext) : base(fakeServer, testContext)
        {
        }

        [Then(@"Le serveur me retourne une erreur (.*) avec le message ""(.*)""")]
        public void ThenLeServeurMeRetourneUneErreurAvecLeMessage(int errorCode, string errorMessage)
        {
            TestContext.HasError.Should().BeTrue();
            TestContext.Error.StatusCode.Should().Be(errorCode);
            TestContext.Error.Message.Should().Be(errorMessage);
        }
    }
}