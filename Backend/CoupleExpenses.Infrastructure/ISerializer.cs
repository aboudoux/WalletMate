namespace WalletMate.Infrastructure
{
    public interface ISerializer
    {
        string Serialize(object value);
        object Deserialize(string value);
    }
}