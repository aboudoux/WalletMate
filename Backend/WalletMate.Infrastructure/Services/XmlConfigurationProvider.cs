using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
                var config = new WalletMateConfiguration();
                config.FirstPair = new User("FirstUserName", "password");
                config.SecondPair = new User("SecondUserName", "password");
                config.Operator = new User("OperatorUserName", "password");
                
                Xml.SaveTo(configurationFile, config);

                throw new FileNotFoundException("La configuration des utilisateurs n'a pas été trouvée. Un fichier a été généré pour que vous puissiez l'éditer");
            }

        }

        public IReadOnlyCollection<User> GetUsers()
        {
            throw null;
        }
    }
}