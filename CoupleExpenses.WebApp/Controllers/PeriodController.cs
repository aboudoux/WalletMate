using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Application.Periods;
using CoupleExpenses.Application.Periods.Queries;
using CoupleExpenses.Domain.Periods.ValueObjects;
using CoupleExpenses.WebApp.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleExpenses.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PeriodController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public PeriodController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
        }

        [HttpGet("[action]")]
        public async Task<IReadOnlyList<string>> All()
        {
            return await _queryBus.QueryAsync<IReadOnlyList<string>>(new GetAllPeriod());
        }

        [HttpGet("[action]")]
        public IEnumerable<PeriodOperation> Operations() 
        {
            return new List<PeriodOperation>() {
                new PeriodOperation(
                    "Dépense",
                    "Aurélien",
                    18.5,
                    "My Leclerc drive " + DateTime.Now,
                    "Avance")
            };
        }

        [HttpPost("[action]")]
        public IActionResult CreateNext()
        {
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody]Period input)
        {
            try
            {
                await _commandBus.SendAsync(new CreatePeriod(PeriodName.From(input.Month, input.Year)));
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
