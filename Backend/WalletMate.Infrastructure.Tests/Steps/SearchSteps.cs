using System.Collections.Generic;
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
    public sealed class SearchSteps : StepBase
    {
        private IEnumerable<IPeriodOperation> _operations;

        public SearchSteps(FakeServer fakeServer, TestContext testContext) : base(fakeServer, testContext)
        {
        }

        [When(@"Je lance une recherche d'opérations avec le filtre ""(.*)""")]
        public async Task WhenJeLanceUneRechercheDOperationsAvecLeFiltre(string filter)
        {
            _operations = await FakeServer.SearchOperation(filter);
        }

        [Then(@"La liste des opérations trouvées est")]
        public void ThenLaListeDesOperationsTrouveesEst(PeriodOperation[] operations)
        {
            _operations.Should().NotBeEmpty().And.HaveCount(operations.Length);
            _operations.ForEach(e => operations.Should().ContainEquivalentOf(e));
        }

        [Then(@"La liste des opérations trouvées est vide")]
        public void ThenLaListeDesOperationsTrouveesEstVide()
        {
            _operations.Should().BeEmpty();
        }
    }
}