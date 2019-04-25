using System;

namespace WalletMate.Infrastructure.Services
{
    public class EmptyConfigurationPasswordException : Exception
    {
        public EmptyConfigurationPasswordException() : base("Il manque un mot de passe dans le fichier de configuration.")
        {
            
        }
    }
}