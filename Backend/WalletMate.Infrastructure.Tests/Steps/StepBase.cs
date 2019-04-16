using System;
using WalletMate.Infrastructure.Tests.Assets;

namespace WalletMate.Infrastructure.Tests.Steps
{
    public abstract class StepBase
    {
        protected readonly TestContext TestContext;
        protected readonly FakeServer FakeServer;

        protected StepBase(FakeServer fakeServer, TestContext testContext )
        {
            TestContext = testContext ?? throw new ArgumentNullException(nameof(testContext));
            FakeServer = fakeServer ?? throw new ArgumentNullException(nameof(fakeServer));
        }
    }
}