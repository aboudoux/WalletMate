using System;

namespace WalletMate.Infrastructure.Services.Exceptions
{
    public class EmptyConfigurationUsernameException : Exception
    {
        public EmptyConfigurationUsernameException() : base("Il manque un nom d'utilisateur dans le fichier de configuration.")
        {
            
        }
    }
}