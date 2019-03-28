using System;
using System.Collections.Generic;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Periods.ValueObjects;
using CoupleExpenses.WebApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoupleExpenses.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PeriodController : Controller
    {
        private static List<string> _periods = new List<string>();

        [HttpGet("[action]")]
        public IEnumerable<string> All()
        {
            return _periods;
        }

        [HttpGet("[action]")]
        public IEnumerable<PeriodOperation> Operations() 
        {
            return new List<PeriodOperation>() {
                new PeriodOperation() {
                    Type = "Dépense",
                    Pair = "Aurélien",
                    Amount = 18.5,
                    Label = "My Leclerc drive " + DateTime.Now,
                    OperationType = "Avance"
                }
            };
        }

        [HttpPost("[action]")]
        public IActionResult CreateNext()
        {
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]Period input)
        {
            var periodName = PeriodName.From(input.Month, input.Year);
            _periods.Add(periodName.ToString());
            return Ok();
        }
    }
}
