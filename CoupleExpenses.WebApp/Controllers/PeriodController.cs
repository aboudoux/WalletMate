using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleExpenses.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PeriodController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<string> All()
        {
            return new List<string>() { "Mars 2019", "Avril 2019", "Mai 2019", "June 2019"};
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

    }

    public class PeriodOperation 
    {
        public string Type {get; set;}
        public string Pair{get; set;}           
        public double Amount{get; set;}
        public string Label{get; set;}
        public string OperationType{get; set;}
    }
}
