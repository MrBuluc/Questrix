using Microsoft.Extensions.Configuration;

namespace Questrix.Persistence
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();

                configurationManager.SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\\Infrastructure\\Questrix.Persistence");
                configurationManager.AddJsonFile("PrivateInformations.json");
                return configurationManager.GetConnectionString("PostgreSQL");
            }
        }
    }
}
