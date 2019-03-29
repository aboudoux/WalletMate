using System.Threading.Tasks;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Application.Core
{
    public interface IQueryBus
    {
        Task<TResult> QueryAsync<TResult>(IRequest query);
    }
}