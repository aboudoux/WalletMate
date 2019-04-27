using System;
using System.Threading;
using System.Threading.Tasks;
using WalletMate.Application.Core;

namespace WalletMate.Application.Pairs
{
    public class PairQueryHandler :
        IQueryHandler<GetConfiguredPair, IConfiguredPair>
    {
        private readonly IUserProvider _userProvider;

        public PairQueryHandler(IUserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public Task<IConfiguredPair> Handle(GetConfiguredPair request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userProvider.GetConfiguredPair());
        }
    }
}