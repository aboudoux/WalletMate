using System;

namespace WalletMate.Infrastructure.Dto
{
    [Serializable]
    public class User
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User()
        {
            
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}