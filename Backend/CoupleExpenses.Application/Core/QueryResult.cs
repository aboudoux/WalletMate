using Mediator.Net.Contracts;

namespace CoupleExpenses.Application.Core
{
    public class QueryResult<T> : IResponse
    {
        public QueryResult(T result)
        {
            Result = result;
        }

        public T Result { get; }
    }
}