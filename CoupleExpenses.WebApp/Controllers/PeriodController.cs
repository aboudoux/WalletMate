using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletMate.Application.Core;
using WalletMate.Application.Periods;
using WalletMate.Application.Periods.Queries;
using WalletMate.Domain.Periods.ValueObjects;
using WalletMate.Infrastructure.Dto;

namespace WalletMate.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PeriodController : ControllerBase
    {
        private readonly IQueryBus _queryBus;

        public PeriodController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus)
        {
            _queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
        }

        [HttpGet("[action]")]
        public async Task<IReadOnlyList<IPeriodResult>> All()
        {
            return await _queryBus.QueryAsync(new GetAllPeriod());
        }

        [HttpGet("[action]")]
        public async Task<IPeriodBalance> Balance(string periodId)
        {
            return await _queryBus.QueryAsync(new GetPeriodBalance(PeriodId.From(periodId)));
        }

        [HttpPost("[action]")]
        public IActionResult CreateNext()
        {
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody]Period input)
        {
            return await SendCommandAsync(new CreatePeriod(PeriodName.From(input.Month, input.Year)));
        }
    }
}
