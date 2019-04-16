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
    public class OperationController : ControllerBase
    {
        private readonly IQueryBus _queryBus;

        public OperationController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus)
        {
            _queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddSpending([FromBody]Spending input)
        {
            return await SendCommandAsync(new AddSpending(
                PeriodId.From(input.PeriodId),
                Amount.From(input.Amount),
                Label.From(input.Label),
                Pair.From(input.Pair),
                SpendingCategory.From(input.Category)));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRecipe([FromBody]NewRecipe input)
        {
            return await SendCommandAsync(new AddRecipe(
                    PeriodId.From(input.PeriodId),
                    Amount.From(input.Amount),
                    Label.From(input.Label),
                    Pair.From(input.Pair),
                    RecipeCategory.From(input.Category)));            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeRecipe([FromBody]UpdateRecipe input)
        {
            return await SendCommandAsync(new ChangeRecipe(
                    PeriodId.From(input.PeriodId),
                    OperationId.From(input.OperationId),
                    Amount.From(input.Amount),
                    Label.From(input.Label),
                    Pair.From(input.Pair),
                    RecipeCategory.From(input.Category)));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeSpending([FromBody]UpdateSpending input)
        {
            return await SendCommandAsync(new ChangeSpending(
                PeriodId.From(input.PeriodId),
                OperationId.From(input.OperationId),
                Amount.From(input.Amount),
                Label.From(input.Label),
                Pair.From(input.Pair),
                SpendingCategory.From(input.Category)));
        }

        [HttpGet("[action]")]
        public async Task<IReadOnlyList<IPeriodOperation>> All(string periodId)
        {
            return await _queryBus.QueryAsync(new GetAllOperation(PeriodId.From(periodId)));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Remove([FromBody]OperationToRemove input)
        {
            return await SendCommandAsync(new RemoveOperation(PeriodId.From(input.PeriodId),OperationId.From(input.OperationId)));            
        }
    }
}