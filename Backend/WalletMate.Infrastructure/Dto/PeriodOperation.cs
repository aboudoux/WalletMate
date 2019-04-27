using Newtonsoft.Json;
using WalletMate.Application.Periods.Queries;
using WalletMate.Domain.Periods.Events;

namespace WalletMate.Infrastructure.Dto
{
    public class PeriodOperation : IPeriodOperation
    {
        [JsonConstructor]
        public PeriodOperation(string periodId, int operationId, string type, string pair, int pairValue, double amount, string label, string category, int categoryValue)
        {
            PeriodId = periodId;
            OperationId = operationId;
            Type = type;
            Pair = pair;
            PairValue = pairValue;
            Amount = amount;
            Label = label;
            Category = category;
            CategoryValue = categoryValue;
        }

        public PeriodOperation(SpendingAdded @event, string pairName)
        {
            Type = "Dépense";
            PeriodId = @event.AggregateId;
            OperationId = @event.OperationId.Value;
            Pair = pairName;
            PairValue = @event.Pair.Value;
            Amount = @event.Amount.Value;
            Label = @event.Label.Value;
            Category = @event.Category.ToString();
            CategoryValue = @event.Category.Value;
        }

        public PeriodOperation(RecipeAdded @event, string pairName)
        {
            Type = "Recette";
            PeriodId = @event.AggregateId;
            OperationId = @event.OperationId.Value;
            Pair = pairName;
            PairValue = @event.Pair.Value;
            Amount = @event.Amount.Value;
            Label = @event.Label.Value;
            Category = @event.Category.ToString();
            CategoryValue = @event.Category.Value;
        }

        public string PeriodId { get; }
        public int OperationId { get; }
        public string Type { get; }        
        public string Pair{get; set; }
        public int PairValue { get; set; }
        public double Amount{get; set; }
        public string Label{get; set; }
        public string Category{get; set; }
        public int CategoryValue { get; set; }
    }
}