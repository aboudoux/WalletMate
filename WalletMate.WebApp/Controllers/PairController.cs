using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletMate.Application.Core;
using WalletMate.Application.Pairs;
using WalletMate.Application.Pairs.Queries;

namespace WalletMate.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class PairController
    {
        private readonly IQueryBus _queryBus;

        public PairController(IQueryBus queryBus)
        {
            _queryBus = queryBus;
        }

        [HttpGet("[action]")]
        public async Task<IConfiguredPair> All()
        {
            return await _queryBus.QueryAsync(new GetConfiguredPair());
        }
    }
}