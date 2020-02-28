using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletMate.Application.Core;
using WalletMate.Application.Pairs;
using WalletMate.Domain.Common;

namespace WalletMate.Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly Dictionary<Guid, string> _connectedUsers = new Dictionary<Guid, string>();
        private readonly IReadOnlyCollection<IUser> _users;

        public AuthorizationService(IUserProvider user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            _users = user.GetUsers();
        }

        public Task<Guid> Authenticate(string username, string password)
        {
	        if (password.IsEmpty()) return Task.FromResult(Guid.Empty);
            var hashedPassword = password.ToSha1();
            if (_users.Any(x =>
                string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(x.Password, hashedPassword, StringComparison.CurrentCultureIgnoreCase)))
            {
                var authKey = Guid.NewGuid();
                _connectedUsers.Add(authKey, username);
                return Task.FromResult(authKey);
            }

            return Task.FromResult(Guid.Empty);
        }

        public Task<string> GetAssociatedUser(Guid authKey)
        {
            if (_connectedUsers.ContainsKey(authKey))
                return Task.FromResult(_connectedUsers[authKey]);
            return Task.FromResult(string.Empty);
        }

        public Task Logoff(Guid authKey)
        {
            if (_connectedUsers.ContainsKey(authKey))
                _connectedUsers.Remove(authKey);
            return Task.CompletedTask;
        }
    }   
}