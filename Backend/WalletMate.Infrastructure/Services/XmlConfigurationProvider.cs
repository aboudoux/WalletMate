using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using WalletMate.Domain.Common;
using WalletMate.Infrastructure.Dto;

namespace WalletMate.Infrastructure.Services
{
    public class XmlConfigurationProvider : IConfigurationProvider
    {       
        public XmlConfigurationProvider(string configurationFile = null)
        {
            if (configurationFile.IsEmpty())
            {
                var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                configurationFile = Path.Combine(currentDirectory, "Users.config");
            }

            if (!File.Exists(configurationFile))
            {
                CreateDefaultConfiguration();
                throw new FileNotFoundException("La configuration des utilisateurs n'a pas été trouvée. Un fichier a été généré pour que vous puissiez l'éditer");
            }

            var configuration = ConfigurationDeserializer.Deserialize(configurationFile);
            

            void CreateDefaultConfiguration()
            {
                var config = new WalletMateConfiguration();
                config.FirstPair = new User("FirstUserName", "password");
                config.SecondPair = new User("SecondUserName", "password");
                config.Operator = new User("OperatorUserName", "password");
                Xml.SaveTo(configurationFile, config);
            }
        }

        public IReadOnlyCollection<User> GetUsers()
        {
            throw null;
        }

       
    }

    public class ConfigurationDeserializer
    {

        public static WalletMateConfiguration Deserialize(string configurationFile)
        {
            var configuration = Xml.DeserializeFrom<WalletMateConfiguration>(configurationFile);
            if(configuration.EncodePlainTextPasswords())
                Xml.SaveTo(configurationFile, configuration);

            return configuration;

        }        
    }
}
