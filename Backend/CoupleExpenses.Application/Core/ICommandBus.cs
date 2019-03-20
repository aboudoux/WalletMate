using System.Threading.Tasks;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Application.Core
{
    public interface ICommandBus
    {
        Task SendAsync(ICommand command);
    }
}