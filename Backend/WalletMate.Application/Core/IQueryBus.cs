using System.Threading.Tasks;
using MediatR;

namespace WalletMate.Application.Core
{
    public interface IQueryBus
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }

    public interface IQuery<out T> : IRequest<T>
    {
        
    }

    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> 
        where TQuery : IQuery<TResult>
    {
        
    }
}