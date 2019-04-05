using CoupleExpenses.Application.Periods.Queries;
using CoupleExpenses.Domain.Periods.Events;
using Newtonsoft.Json;

namespace CoupleExpenses.Infrastructure.Dto
{
    public class PeriodOperation : IPeriodOperation
    {
        [JsonConstructor]
        public PeriodOperation(string periodId, int operationId, string type, string pair, double amount, string label, string category)
        {
            PeriodId = periodId;
            OperationId = operationId;
            Type = type;
            Pair = pair;
            Amount = amount;
            Label = label;
            Category = category;
        }

        public PeriodOperation(SpendingAdded @event)
        {
            PeriodId = @event.AggregateId;
            OperationId = @event.OperationId.Value;
            Type = "Dépense";
            Pair = @event.Pair.ToString();
            Amount = @event.Amount.Value;
            Label = @event.Label.Value;
            Category = @event.Category.ToString();
        }

        public PeriodOperation(RecipeAdded @event)
        {
            PeriodId = @event.AggregateId;
            OperationId = @event.OperationId.Value;
            Type = "Recette";
            Pair = @event.Pair.ToString();
            Amount = @event.Amount.Value;
            Label = @event.Label.Value;
            Category = @event.Category.ToString();
        }

        public string PeriodId { get; }
        public int OperationId { get; }
        public string Type { get; }        
        public string Pair{get; set; }           
        public double Amount{get; set; }
        public string Label{get; set; }
        public string Category{get; set; }
    }
}