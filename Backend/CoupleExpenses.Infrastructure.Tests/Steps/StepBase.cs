using System;
using CoupleExpenses.Infrastructure.Tests.Assets;

namespace CoupleExpenses.Infrastructure.Tests.Steps
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