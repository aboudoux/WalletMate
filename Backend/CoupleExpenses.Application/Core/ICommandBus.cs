using System.Threading.Tasks;
using MediatR;

namespace WalletMate.Application.Core
{
    public interface ICommandBus
    {
        Task SendAsync(ICommand command);
    }

    public interface ICommand : INotification
    {
        
    }

    public interface ICommandHandler<in TCommand> : INotificationHandler<TCommand>
        where TCommand : ICommand
    {
        
    }        
}