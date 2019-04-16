namespace WalletMate.Infrastructure.Tests.Assets
{
    public class TestContext
    {
        public void AddError(HttpServerError error)
        {
            Error = error;
        }

        public bool HasError => Error != null;
        public HttpServerError Error { get; private set; }
    }
}