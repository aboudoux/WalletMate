using WalletMate.Infrastructure.Services.Exceptions;

namespace WalletMate.Infrastructure.Services
{
    public class ConfigurationDeserializer
    {

        public static WalletMateConfiguration Deserialize(string configurationFile)
        {
            var configuration = Xml.DeserializeFrom<WalletMateConfiguration>(configurationFile);

            if (configuration.AnyNameMissing())
                throw new EmptyConfigurationUsernameException();
            if (configuration.AnyPasswordMissing())
                throw new EmptyConfigurationPasswordException();

            if (configuration.EncodePlainTextPasswords())
                Xml.SaveTo(configurationFile, configuration);

            return configuration;

        }        
    }
}