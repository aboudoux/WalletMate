namespace CoupleExpenses.Infrastructure.Dto
{
    public class User
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; }
        public string Password { get; }
    }
}