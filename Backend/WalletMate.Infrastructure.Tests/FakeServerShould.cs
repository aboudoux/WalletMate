using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using WalletMate.Infrastructure.Tests.Assets;
using Xunit;

namespace WalletMate.Infrastructure.Tests
{
    public class FakeServerShould
    {
        [Fact]
        public async Task Call_authenticate_with_success()
        {
            using (var server = new FakeServer())
            {
                var reponse = await server.Authenticate("Aurélien", "1234");
                reponse.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Add_new_period_with_authenticated_user()
        {
            using (var server = new FakeServer())
            {
                await server.Authenticate("Aurélien", "1234");
                var response = await server.CreatePeriod(1, 2000);
                response.Should().Be(HttpStatusCode.OK);
            }
        }      
    }
}