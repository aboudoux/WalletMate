using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WalletMate.Infrastructure.Tests.Assets;

namespace WalletMate.Infrastructure.Tests.Steps
{
    [Binding]
    public sealed class AuthenticationSteps : StepBase
    {
        public AuthenticationSteps(FakeServer fakeServer, TestContext testContext) : base(fakeServer, testContext)
        {
        }

        [Given(@"Je suis connecté à l'application avec l'utilisateur (.*) et le mot de passe (.*)")]
        public async Task GivenJeSuisConnecteALUtilisateurAurelienEtLeMotDePasse(string username, string password)
        {
            await FakeServer.Authenticate(username, password);
        }
    }
}
