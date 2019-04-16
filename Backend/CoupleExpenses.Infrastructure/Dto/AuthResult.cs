using System;

namespace WalletMate.Infrastructure.Dto
{
    public class AuthResult
    {
        public AuthResult(string username, Guid authKey)
        {
            Username = username;
            AuthKey = authKey;
        }

        public string Username { get; }
        public Guid AuthKey { get; }
    }
}