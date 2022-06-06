using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSale.ConsoleApp
{
    internal class ConfigBuilder
    {
        public static IConfigurationRoot Build(bool mockApi = false)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddInMemoryCollection(
                new List<KeyValuePair<string, string>>()
                {
                   new KeyValuePair<string, string>("MOCK_API",mockApi.ToString())
                });
            var config = configBuilder.Build();
            return config;
        }
    }
}
