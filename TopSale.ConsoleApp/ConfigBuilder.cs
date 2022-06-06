using Microsoft.Extensions.Configuration;

namespace TopSale.ConsoleApp
{
    internal class ConfigBuilder
    {
        public static IConfigurationRoot Build<TSecret>(string[] args) 
            where TSecret : class
        {
            var configBuilder = new ConfigurationBuilder();

            configBuilder.AddUserSecrets<TSecret>();
            configBuilder.AddEnvironmentVariables();
            if (args is not null)
                configBuilder.AddCommandLine(args);

            var config = configBuilder.Build();
            return config;
        }
    }
}
